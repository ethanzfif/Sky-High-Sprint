using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Vector3 oldGrav;
    private int timer = 2100;
    private PlatformManager platformManagerScript;
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        oldGrav = Physics.gravity;
        Physics.gravity *= 1.8f;
        platformManagerScript = GameObject.Find("PlatformManager").GetComponent<PlatformManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= 0)
        {
            //start on screen countdown
            DisplayTime();
            timer-= 2; 
            //spawn platforms
            if (timer == 0)
            {
                timerText.text = "";
                timer = -1;
                platformManagerScript.canSpawnPlatforms = true;
            }
        }
    }

    private void DisplayTime()
    {
        timerText.text = "Game Starts \n in \n" + (timer/100).ToString();
    }
}
