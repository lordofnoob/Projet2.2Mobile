using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDash : MonoBehaviour
{
    public Camera cam;
    public float directionRun;
    public float accelerationRn;
    public float vitesseMaxCourse;

    [Header ("anim")]
    public Animator anim;

    [Header("Twic dash")]
    public float cooldownDash;
    public Rigidbody body;
    public float dashSpeed;
    public float maxLengthSaut;
    public float mass;
    public float drag;


    // Recup vecteur de dash
    float lengthSaut;
    Vector3 beginPose;
    Vector3 direction;
    Vector3 endPos;
    float previsedDashLength;
    float parcoredLength;
    Vector3 posPlayerPreDash;

    

    //check possibilité de dash/declenchement dash
    bool directionChosen = false;
    bool canDash = true;
    bool recordDistance;
    float recordedDistance;


    float distanceToScreen;

    private void Start()
    {
        distanceToScreen = cam.farClipPlane;
        body.drag = drag;
        body.mass = mass;
    }




    void Update()
    {

        if (canDash == true)
            RecupVector();

    

        RecordParcoredDistance();

        if (anim.GetBool("dashing")==false)
        {
            ResetProp();
        }

    }

    void RecupVector()
    {
        if (Input.touchCount > 0)
        {
            /* Vector3 posDepart = Input.GetTouch(0).position;
             transform.Translate(Input.GetTouch(0).deltaPosition* Time.deltaTime);
             Debug.Log(Input.GetTouch(0).position);*/

            Touch touch = Input.GetTouch(0);

           switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        anim.SetBool("prepDash", true);
                        Time.timeScale = 0.2f;

                        beginPose = Input.mousePosition;
                        beginPose.z = distanceToScreen;
                        beginPose = cam.ScreenToWorldPoint(beginPose);
                        
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        Time.timeScale = 0.2f;

                        var movingFinger = Input.mousePosition;
                        movingFinger.z = distanceToScreen;
                        movingFinger= cam.ScreenToWorldPoint(movingFinger);

                        direction = movingFinger - beginPose;

                        lengthSaut = Vector2.Distance(movingFinger, beginPose);

                        if (lengthSaut > maxLengthSaut)
                            lengthSaut = maxLengthSaut;
                    
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        body.velocity = new Vector3(0, 0, 0);
                        Time.timeScale = 2f;    
                        directionChosen = true;


                        StartCoroutine("coolDownDash");
                        canDash = false;

                        anim.SetBool("prepDash", false);
                        anim.SetBool("dashing", true);

                        posPlayerPreDash = transform.position;

                        var endPos = Input.mousePosition;
                        endPos.z = distanceToScreen;
                        endPos = cam.ScreenToWorldPoint(endPos);

                        previsedDashLength =  Vector3.Distance(beginPose, endPos);


                        recordDistance = true;

                        break;
                    }
            }
        }

    }   

    void Dash()
    {
        //  body.velocity = -direction * lengthSaut/6*(dashSpeed/100);
        body.AddForce(-direction *(lengthSaut/2), ForceMode.Impulse);



    }

    void Run()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector2.down), out hit, 2))
        {
            anim.SetBool("Running", true);
            if (body.velocity.x> vitesseMaxCourse || body.velocity.x< -vitesseMaxCourse)
            {
               // body.AddForce(new Vector3());
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }


    IEnumerator coolDownDash()
    {
       
        yield return new WaitForSeconds(cooldownDash);
        print("cd");
        canDash = true;
        if (anim.GetBool("dashing") == false)
            anim.SetBool("dashing", false);

    }



   
    private void RecordParcoredDistance()
    {
        if (recordDistance == true)
        {
            body.useGravity = false;
          //  body.drag = 0.1f;
            body.mass = 1; 
            recordedDistance = Vector3.Distance(posPlayerPreDash, transform.position);

            Debug.Log("Recorded distance : " + recordedDistance + ", Prevised dash lenght : " + previsedDashLength);
            if (recordedDistance > (7/10)*previsedDashLength)
            {
                
                anim.SetBool("dashing",false);
                recordDistance = false;
                recordedDistance = 0;
                previsedDashLength = 0;
            }
            
        }
        else
        {
            recordedDistance = 0;
            previsedDashLength = 0;
        }
    }

    private void ResetProp()
    {
        Time.timeScale = 1f;
        body.useGravity = true;
        body.mass = mass;
        body.drag = drag;
        anim.SetBool("dashing", false);
        recordDistance = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        anim.SetBool("dashing", false);
    }
}
