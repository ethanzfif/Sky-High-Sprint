using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool moveOnX = true;
    private int shiftRange = 8;
    public int speed;
    private Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        //randomize speed
        speed = Random.Range(5, 16);
        //randomize direction
        moveOnX = Random.value > 0.5;
        //randomize sart pos
        int placement = Random.Range(-shiftRange, shiftRange);

        //note spawn pos
        spawnPos = transform.position;
        //move to randomized pos
        if (moveOnX)
        {
            transform.Translate(Vector3.right * placement);
        }
        else
        {
            transform.Translate(Vector3.forward * placement);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveOnX)
        {
            MovePlatform(Vector3.right, transform.position);
        }
        else
        {
            MovePlatform(Vector3.forward, transform.position);
        }
    }

    //needs movement direction and current pos
    //moves platform on movement axis
    private void MovePlatform(Vector3 vector, Vector3 pos)
    {
        //keep platform in range
        if (Mathf.Abs(spawnPos.x - pos.x) >= shiftRange + 1 || Mathf.Abs(spawnPos.z - pos.z) >= shiftRange + 1)
        {
            speed *= -1;
        }
        transform.Translate(vector * speed * Time.deltaTime);
    }

}
