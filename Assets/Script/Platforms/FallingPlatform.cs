using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Vector3 spawnPos;
    private bool falling = true;
    private float fallDelay = 2.0f;
    private float fallTimer;
    private float shakeSpeed = 10.0f;
    private int flickerSpeed = 0;
    private float fallSpeed = 10.0f;
    private float fallDistance = 25.0f;
    // Start is called before the first frame update
    void Start()
    {
        //save pos for falling distance
        spawnPos = transform.position;
        fallTimer = fallDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            //shake to warn players
            fallTimer -= Time.deltaTime;
            if (fallTimer > 0)
            {
                transform.Translate(Vector3.down * shakeSpeed * Time.deltaTime);
                if (flickerSpeed % 2 == 0)
                {   
                    shakeSpeed *= -1;
                }
                flickerSpeed++;

            }
            //drop platform
            else
            {
                //corect pos
                //transform.position = spawnPos;

                transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
            }
            //bring platform back up
            if(transform.position.y < spawnPos.y - fallDistance)
            {
                transform.position = spawnPos;
                //falling = false;
                fallTimer = fallDelay;               
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            falling = true;
        }
    }

    private void Fall()
    {

    }
}
