using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager; //On rend le game manager accessible dans tous les scripts

    [Range(0, 10), SerializeField, Header("Death Restart")]
    public int timeBeforeRestart = 3;   //Temps avant restart
    public CinemachineVirtualCamera cameraBrain; //Camera virtuelle de cinemachine
    public GameObject startingPosition; //Position à laquelle le joueur va réaparaitre

    [Header("BonusPUB")]
    public int bonusPub = 0; //nombre de bonus récupérés
    public Canvas UiBonusPub;

    private void Awake()
    {
        gameManager = this; // accès au game manager dans tout le code
    }
    private void Start()
    {
        PlayerManager.playerManager.transform.position = startingPosition.transform.position;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        UiDisplayBonus();

        if (PlayerManager.playerManager.dead)
        {
            StartCoroutine(RestartDelay());

            /*PlayerManager.playerManager.transform.position = startingPosition.transform.position; //On repositionne le player au point de départ
            cameraBrain.m_Follow = PlayerManager.playerManager.transform; //On repositionne la caméra sur le joueur
            PlayerManager.playerManager.dead = false; //Le joueur n'est plus mort*/
        }
    }

    private void UiDisplayBonus()
    {
        if (bonusPub > 0) //Ui affichage bonusPUB
        {
            UiBonusPub.transform.GetChild(0).gameObject.SetActive(true);
            if (bonusPub > 1)
            {
                UiBonusPub.transform.GetChild(1).gameObject.SetActive(true);
                if (bonusPub > 2)
                {
                    UiBonusPub.transform.GetChild(2).gameObject.SetActive(true);
                }
            }
        }
    }

    IEnumerator RestartDelay()
    {
        yield return new WaitForSeconds(timeBeforeRestart); //Delai avant le restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
