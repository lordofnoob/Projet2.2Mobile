using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FallingManager : MonoBehaviour
{
    public CinemachineVirtualCamera cameraBrain;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == PlayerManager.playerManager.gameObject) //On regarde si le joueur entre dans la zone
        {
            Debug.Log("Trigger Falling Zone");
            cameraBrain.m_Follow = null; //Blocage de la camera à travers la var static
            PlayerManager.playerManager.dead = true; //Joueur mort
            Debug.Log("Player Dead");
        }
    }
}
