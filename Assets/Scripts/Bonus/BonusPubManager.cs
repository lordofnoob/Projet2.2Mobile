using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPubManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == PlayerManager.playerManager.gameObject)
        {
            GameManager.gameManager.bonusPub += 1; //On incrémente le nombre de bonus récupérés
            Destroy(gameObject);
        }
    }
}
