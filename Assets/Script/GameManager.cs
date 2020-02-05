using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 oldGrav;
    private float timer;
    private PlatformManager platformManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        oldGrav = Physics.gravity;
        Physics.gravity *= 1.8f;
        platformManagerScript = GameObject.Find("PlatformManager").GetComponent<PlatformManager>();
        platformManagerScript.canSpawnPlatforms = true;
    }

    // Update is called once per frame
    void Update()
    {
        //start on screen countdown
        //spawn platforms
    }
}
