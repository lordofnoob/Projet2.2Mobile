﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDashReset : MonoBehaviour
{
    public GameObject player;

       private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
<<<<<<< Updated upstream:Assets/Scripts/CollectibleDashReset.cs
            player.GetComponent<PlayerManager>().canDash = true;
=======
            PlayerDash.canDash = true;

>>>>>>> Stashed changes:Assets/CollectibleDashReset.cs
            Destroy(gameObject);
        }
    }

}
