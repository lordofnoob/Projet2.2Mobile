using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDashReset : MonoBehaviour
{

     private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerManager.playerManager.gameObject)
        {
            PlayerManager.playerManager.canDash = true;
            Destroy(gameObject);
        }
    }

}
