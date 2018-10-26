using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{

    //Calcul des FPS

    public float updateInterval = 0.5f;
    public float accum = 0f;
    public float frames = 0f;
    public float timeleft;

    void Start()
    {
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            if (accum / frames < 30)
            {
                transform.GetComponent<Text>().color = Color.red;
            }

            if (accum / frames > 30 && accum / frames < 60)
            {
                transform.GetComponent<Text>().color = Color.yellow;
            }

            if (accum / frames > 60)
            {
                transform.GetComponent<Text>().color = Color.cyan;
            }

            transform.GetComponent<Text>().text = (accum / frames).ToString("f1"); // f1 -> tronque la valeur
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }
}
