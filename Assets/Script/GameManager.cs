using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Vector3 oldGrav;
    private int timer = 2100; //2100
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
            timer-= 2;
            //end popup text
            if (timer == 3000)
            {
                timerText.text = "";
                timer = -1;
            }
            //spawn platforms
            else if (timer < 3000)
            {
                //start on screen countdown
                DisplayTime();
                //end countdown
                if (timer == 0)
                {
                    timerText.text = "";
                    platformManagerScript.canSpawnPlatforms = true;
                }
            }
        }
    }

    private void DisplayTime()
    {
        timerText.text = "Game Starts \n in \n" + (timer/100).ToString();
    }

    public void DisplayVictor(int place)
    {
        if(place == 1)
        {
            timerText.text = "The first player has reached the goal!!!";
            timer = 3200;
        }
        else
        {
            timerText.text = place + " players have reached the goal!";
            timer = 3200;
        }
    }
}
