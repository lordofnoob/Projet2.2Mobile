using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDashReset : MonoBehaviour
{
    public GameObject player;

     private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.GetComponent<PlayerManager>().canDash = true;
            Destroy(gameObject);
        }
    }

}
