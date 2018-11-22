using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingManager : MonoBehaviour
{
    [Header("Target")]
    public GameObject player;

    [Header("Distance")]
    public int distanceMax;
    public int distanceMin;

    [Header("Vitesse")]
    public int vitesseMax;
    public int vitesseMoy;
    public int vitesseMin;

    float distance;



    [Range(1, 100), SerializeField, Header("Speed")]
    private float movementSpeed = 1;

    void Update()
    {
        AdaptSpeed();
        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed); //Deplacement de la zone vers la droite.
    }

    private void OnTriggerEnter(Collider col) //On regarde si le joueur entre dans la zone
    {
        if(col.gameObject == player)
        {
            Debug.Log("Player Dead");
        }
    }

    void AdaptSpeed()
    {
        distance = Mathf.Abs(player.transform.position.x) - Mathf.Abs(transform.position.x);
        if (distance > distanceMax)
            movementSpeed = Mathf.Lerp(movementSpeed, vitesseMax, 0.1f);
        else if (distance <= distanceMax && distance > distanceMin)
            movementSpeed = Mathf.Lerp(movementSpeed, vitesseMoy, 0.1f);
        else if (distance <= distanceMin)
            movementSpeed = Mathf.Lerp(movementSpeed, vitesseMin, 0.1f);
    }
}
