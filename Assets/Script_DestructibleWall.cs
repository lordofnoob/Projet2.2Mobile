using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DestructibleWall : MonoBehaviour
{
    [Range(30, 60), Header("Destructible Wall")]
    public float minimumVelocityToDestroy = 50;

    public ParticleSystem burstingGlass;
    bool brokenWindow = false; //On vérifie si la vitre à déjà été cassée

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Velocity on x axis : " + PlayerManager.playerManager.body.velocity.x);
        // Si le joueur percute la zone on regarde si sa vélocité en x (val abs) est suffisante
        if ((other.gameObject == PlayerManager.playerManager.gameObject) && (Mathf.Abs(PlayerManager.playerManager.body.velocity.x) > minimumVelocityToDestroy))
        {
            if(brokenWindow == false)
            {
                burstingGlass.Play();
                transform.GetChild(0).gameObject.SetActive(false);//On désactive le mur propre
                transform.GetChild(1).gameObject.SetActive(true);//On active le mur détruit
                brokenWindow = true; //La vitre est cassée
            }
        }
    }
}
