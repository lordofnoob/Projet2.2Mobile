using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; //On rend le game manager accessible dans tous les scripts

    [Range(0, 10), SerializeField, Header("Restart")]
    public int timeBeforeRestart = 3;   //Temps avant restart
    public CinemachineVirtualCamera cameraBrain; //Camera virtuelle de cinemachine
    public GameObject startingPosition; //Position à laquelle le joueur va réaparaitre

    // Débloquage des 60 FPS pour le télephone
    private void Start()
    {
        PlayerManager.playerManager.transform.position = startingPosition.transform.position;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        if(PlayerManager.playerManager.dead)
        {
            StartCoroutine(RestartDelay());

            /*PlayerManager.playerManager.transform.position = startingPosition.transform.position; //On repositionne le player au point de départ
            cameraBrain.m_Follow = PlayerManager.playerManager.transform; //On repositionne la caméra sur le joueur
            PlayerManager.playerManager.dead = false; //Le joueur n'est plus mort*/
        }
    }
    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(timeBeforeRestart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
