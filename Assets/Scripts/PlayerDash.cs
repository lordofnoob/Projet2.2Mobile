using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDash : MonoBehaviour
{
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


    // Recup vecteur de dash
    float lengthSaut;
    Vector2 beginPose;
    Vector2 direction;
    Vector2 endPos;
    float previsedDashLength;
    float parcoredLength;

    //check possibilité de dash/declenchement dash
    bool directionChosen = false;
    bool canDash = true;



    void Update()
    {
       
        if (canDash==true)
            RecupVector();
        parcoredLength = Vector2.Distance(endPos, transform.position);
        CheckDashLength();

    }



    private void FixedUpdate()
    {


       // CheckDash();
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
                        beginPose = Input.GetTouch(0).position;
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        Time.timeScale = 0.2f;
                        direction = touch.position - beginPose;
                        lengthSaut = Vector2.Distance(touch.position, beginPose);
                        if (lengthSaut > maxLengthSaut)
                            lengthSaut = maxLengthSaut;
                    
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        body.velocity = new Vector3(0, 0, 0);
                        Time.timeScale = 1f;
                        canDash = false;
                        directionChosen = true;
                        StartCoroutine("coolDownDash");

                        anim.SetBool("prepDash", false);
                        anim.SetBool("dashing", true);
                        playerPos=
                        endPos = Input.GetTouch(0).position;
                        previsedDashLength =  Vector2.Distance(beginPose, endPos);
                        break;
                    }
            }
        }

    }

    void Dash()
    {
        //  body.velocity = -direction * lengthSaut/6*(dashSpeed/100);
        body.AddForce(-direction * lengthSaut/1280);
        print(body.velocity);


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
        canDash = true;
    }

    void CheckDashLength()
    {

        print("aka");
        if (parcoredLength> previsedDashLength)
        {
            previsedDashLength = 0;
            endPos = ;
            anim.SetBool("dashing", false);
        }
    }

    void Gravity()
    {/*
        body.velocity += new Vector3(0, forceGravite, 0);
        if (body.velocity.y < -10)
            body.velocity = new Vector3(body.velocity.x, -10, 0);

        if (body.velocity.x>0)
            body.velocity = new Vector3(body.velocity.x - 0.02f, body.velocity.y, 0);
        else if (body.velocity.x<0)
            body.velocity = new Vector3(body.velocity.x + 0.02f, body.velocity.y, 0);*/
    }



}
