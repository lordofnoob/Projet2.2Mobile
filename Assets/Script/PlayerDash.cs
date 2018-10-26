using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDash : MonoBehaviour
{

    [Header ("anim")]
    public Animator anim;

    [Header("Twic dash")]
    public float cooldownDash;
    public Rigidbody body;
    public float dashSpeed;


    // Recup vecteur de dash
    float lengthSaut;
    Vector2 posDepart;
    Vector2 direction;

    //check possibilité de dash/declenchement dash
    bool directionChosen = false;
    bool canDash = true;



    void Update()
    {
        if (canDash==true)
            RecupVector();

       // CheckBump();

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
                        posDepart = Input.GetTouch(0).position;
                        break;
                    }
                case TouchPhase.Moved:
                    {
                        Time.timeScale = 0.2f;
                        direction = touch.position - posDepart;
                        lengthSaut = Vector2.Distance(touch.position, posDepart);
                        if (lengthSaut > 300 )
                            lengthSaut = 300;
                    
                        break;
                    }
                case TouchPhase.Ended:
                    {
                        body.velocity = new Vector3(0, 0, 0);


                        print(lengthSaut);
                        Time.timeScale = 1f;

                        anim.SetBool("prepDash", false);
                        anim.SetBool("dashing", true);
                        directionChosen = true;
                        StartCoroutine("coolDownDash");
                        canDash = false;
                        print(direction.normalized);
               
                        break;
                    }
            }
        }

    }

    void Dash()
    {
        //  body.velocity = -direction * lengthSaut/6*(dashSpeed/100);
        body.AddForce(-direction * lengthSaut / 120);
        print(body.velocity);

    }

    void DirectDash()
    {
       //body     
    }


    IEnumerator coolDownDash()
    {
        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
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
