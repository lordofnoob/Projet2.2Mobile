using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScrollingManager : MonoBehaviour
{
    [Header("Distance")]
    public int distanceMax;
    public int distanceMin;

    [Header("Vitesse")]
    public int vitesseMax;
    public int vitesseMoy;
    public int vitesseMin;

    public CinemachineVirtualCamera cameraBrain;

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
        if(col.gameObject == PlayerManager.playerManager.gameObject) //reference au joueur par la var static
        {
            cameraBrain.m_Follow = null; //blocage de la camera par une var static
            PlayerManager.playerManager.dead = true;
            Debug.Log("Player Dead");
        }
    }

    void AdaptSpeed() // déplacement de la zone ralenti si elle est proche du joueur
    {
        distance = Mathf.Abs(PlayerManager.playerManager.transform.position.x) - Mathf.Abs(transform.position.x);
        if (distance > distanceMax)
            movementSpeed = Mathf.Lerp(movementSpeed, vitesseMax, 0.1f);
        else if (distance <= distanceMax && distance > distanceMin)
            movementSpeed = Mathf.Lerp(movementSpeed, vitesseMoy, 0.1f);
        else if (distance <= distanceMin)
            movementSpeed = Mathf.Lerp(movementSpeed, vitesseMin, 0.1f);
    }
}
