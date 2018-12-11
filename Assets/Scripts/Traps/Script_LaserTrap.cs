using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_LaserTrap : MonoBehaviour
{
    [Header("Laser")]
    public int activationFrequency = 2;
    public int desactivationDuration = 2;

    bool laserActive = true;


    void Start()
    {
        StartCoroutine(LaserManagement());
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject == PlayerManager.playerManager.gameObject) && laserActive == true)
        {
            Debug.Log("damages by laser");
            GameManager.gameManager.cameraBrain.m_Follow = null; //Blocage de la camera à travers la var static
            PlayerManager.playerManager.dead = true; //Joueur mort
            Debug.Log("Player Dead");
        }
    }

    IEnumerator LaserManagement()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(activationFrequency);
            transform.GetChild(1).gameObject.SetActive(false);
            laserActive = false;
            yield return new WaitForSecondsRealtime(desactivationDuration);
            transform.GetChild(1).gameObject.SetActive(true);
            laserActive = true;
        }
    }

}
