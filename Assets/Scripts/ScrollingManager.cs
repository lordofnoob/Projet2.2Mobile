using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    [Header("Target")]
    public GameObject Player;

    [Range(1, 100), SerializeField, Header("Speed")]
    private float movementSpeed = 1;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed); //Deplacement de la zone vers la droite.
    }

    private void OnTriggerEnter(Collider col) //On regarde si le joueur entre dans la zone
    {
        if(col.gameObject == Player)
        {
            Debug.Log("Player Dead");
        }
    }
}
