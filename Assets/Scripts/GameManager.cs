using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        // Débloquer les 60 FPS pour le télephone
        private void start()
        {
            Application.targetFrameRate = 60;
        }
}
