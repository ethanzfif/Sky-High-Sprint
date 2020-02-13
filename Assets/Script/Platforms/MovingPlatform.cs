using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool moveOnX = true;
    private int shiftRange = 9;
    public int speed;
    private float spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        //randomize speed
        speed = Random.Range(5, 16);
        //randomize direction
        moveOnX = Random.value > 0.5;
        //randomize sart pos
        int placement = Random.Range(-shiftRange, shiftRange);

        //note spawn pos and move to randomized pos
        if (moveOnX)
        {
            spawnPos = transform.position.x;
            transform.Translate(Vector3.right * placement);
        }
        else
        {
            spawnPos = transform.position.z;
            transform.Translate(Vector3.forward * placement);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveOnX)
        {
            MovePlatform(Vector3.right, transform.position.x);
        }
        else
        {
            MovePlatform(Vector3.forward, transform.position.z);
        }
    }

    //needs movement direction and current pos
    //moves platform on movement axis
    private void MovePlatform(Vector3 vector, float pos)
    {
        //keep platform in range
        if (pos > (spawnPos + shiftRange) || pos < (spawnPos - shiftRange))
        {
            speed *= -1;
        }
        transform.Translate(vector * speed * Time.deltaTime);
    }

}
