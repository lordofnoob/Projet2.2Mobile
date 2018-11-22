using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDash : MonoBehaviour
{
    public static bool dead=false;

    public Camera cam;
    public int speed;
    int sens=1;
    int basicSpeed;
    public GameObject fleche;
    public ParticleSystem particlePrepDash;

    #region 
    [Header("anim")]
    public Animator anim;

    [Header("Twic dash")]
    public float cooldownDash;
    public Rigidbody body;
    public float minLengthSaut;
    public float maxLengthSaut;
    public float mass;
    public float drag;
    public AnimationCurve curve;
    #endregion

    #region DashInfo
    float lengthSaut;
    Vector3 direction;
    bool canDash = true;
    //check possibilité de dash/declenchement dash
    bool directionChosen = false;
    //mesure de la distance a dash
    Vector3 beginPos;
    Vector3 endPos;
    //Apres dash mesure pour verifier
    float previsedDashLength;
    float parcoredLength;
    Vector3 posPlayerPreDash;
    bool recordDistance;
    float recordedDistance;
    Vector3 oldPos;
    #endregion

    float distanceToScreen;

    private void Start()
    {
        distanceToScreen = cam.farClipPlane;
        body.drag = drag;
        body.mass = mass;
    }

    void Update()
    {
        Debug.Log(dead);
        if (dead == false)
        {
            // check de distance avec le sol pour run 
            Run();
            // check de possibilité de dash
            if (canDash == true)
                RecupVector();
            //check si le dash effectué 
            RecordParcoredDistance();
            //reset des propriétés si le joueur ne dash et ne s'appréte pas a dasher
            /*if (anim.GetBool("dashing")==false && anim.GetBool("prepDash")==false)
            {
                ResetProp();
            }
            */
            //print(recordDistance);
        }
        else
        {
            anim.SetBool("dying", true);
            anim.SetBool("running", false);
        }
    }

    //le faire arreter de dash quand il rentre en collision avec un autre objet
    void RecupVector()
    {
        if (Input.touchCount > 0 && canDash == true)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        anim.SetBool("prepDash", true);
                        Time.timeScale = 0.2f;
                        //conversion du touch en unité unity
                        beginPos = Input.mousePosition;
                        beginPos.z = distanceToScreen;
                        beginPos = cam.ScreenToWorldPoint(beginPos);
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        Time.timeScale = 0.2f;

                        var movingFinger = Input.mousePosition;
                        movingFinger.z = distanceToScreen;
                        movingFinger = cam.ScreenToWorldPoint(movingFinger);

                        //récup du vecteur tracé entre le moment ou il démarre a toucher et au moment ou il va lacher
                        //direction
                        direction = movingFinger - beginPos;
                        //longueur
                        lengthSaut = Vector2.Distance(movingFinger, beginPos);
                        
                        
                        //maitrise de la longueur maximale 
                        FlecheUpdate();
                        if (lengthSaut > maxLengthSaut)
                            lengthSaut = maxLengthSaut;
                        /* if (lengthSaut < minLengthSaut)
                             lengthSaut = minLengthSaut;
                             */

                        break;
                       
                    }
                case TouchPhase.Ended:
                    {
                        //reset de la vélocité pour faire un gros paf lors du dash
                        body.velocity = new Vector3(0, 0, 0);
                        body.useGravity = false;
                        Time.timeScale = 1;
                        directionChosen = true;

                        //empecher le joueur de redash avec un cd et indication qu'il ne peut pas dash
                        StartCoroutine("coolDownDash");
                        canDash = false;
                        //anim 
                        anim.SetBool("prepDash", false);
                        anim.SetBool("dashing", true);

                        //sauvegarde de la pos pour le check de la distance dashée
                        posPlayerPreDash = transform.position;

                        //conversion du touch en unité unity
                        var endPos = Input.mousePosition;
                        endPos.z = distanceToScreen;
                        endPos = cam.ScreenToWorldPoint(endPos);

                        //anticipation de la longueur a dasher
                        previsedDashLength = Vector3.Distance(beginPos, endPos);

                        //lancement de la fonction pour le check de la distance dashée
                        recordDistance = true;

                        //changement du sens de la course en fonction de la direction du dash
                        if (-direction.x >= 0)
                            sens = 1;
                        else
                            sens = -1;
                        canDash = false;
                        break;
                       
                    }
            }
        }

    }

    void Dash()
    {
        body.drag = 0;
        body.mass = 1;
        body.AddForce(-direction * 1.5f * curve.Evaluate(lengthSaut), ForceMode.Impulse);
    }

    //reset des propriétés si il est se     nsé ne rien avoir de changé
    private void ResetProp()
    {
        Time.timeScale = 1f;
        body.useGravity = true;
        body.mass = mass;
        body.drag = drag;
        anim.SetBool("dashing", false);
        recordDistance = false;
    }

    //sens = 1 droite sens = -1 gauche

    void Run()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.down), out hit, 1.5f) && anim.GetBool("prepDash") == false && anim.GetBool("dashing") == false)
        {
            anim.SetBool("running", true);
            body.velocity = new Vector2(speed*sens, 0);    
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    private void RecordParcoredDistance()
    {
        if (recordDistance == true)
        {
            recordedDistance += Vector3.Distance(oldPos, transform.position);
        

            //  Debug.Log("Recorded distance : " + recordedDistance + ", Prevised dash lenght : " + previsedDashLength);
            //verification de la distance dashé 
            if (recordedDistance > previsedDashLength)
            {
                anim.SetBool("dashing", false);
                recordDistance = false;
                //recordedDistance = 0;
               // previsedDashLength = 0;
                coolDownDash();
                ResetProp();
            }
        }
    }

    private void LateUpdate()
    {
        oldPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        sens = -sens;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ResetProp();
    }

    void FlecheUpdate()
    {
        Vector3 angle = Vector3.RotateTowards(transform.position, direction, 360,0);
       //print(angle);
    }

    IEnumerator coolDownDash()
    {
        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
    }

    void ParticlePrepDashOn()
    {
        particlePrepDash.enableEmission=true;
    }
    void ParticlePrepDashOff()
    {
        particlePrepDash.enableEmission = false;
    }

}
