using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private Vector3 placementPos;
    //x or z distance
    private float platformSpacing = 15.0f;
    //y distance
    private float platformRising = 2.0f;
    //0 = up, 1 = right, 2 = down, 3 = left
    private int oldDirection = 0;
    private int newDirection = 0;
    public GameObject[] platformList;
    public GameObject goal;
    public GameObject checkpoint;
    private GameObject recentPlatform;
    public bool canSpawnPlatforms = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (canSpawnPlatforms)
        {
            SpawnPlatforms(30);
            canSpawnPlatforms = false;
        }
    }

    //needs platformCount
    //spawns platforms
    public void SpawnPlatforms(int platformCount)
    {
        //find spawn pos 1
        placementPos = transform.position;
        //place platforms needed
        for (int i = 1; i < platformCount; i++)
        {
            nextSpawnPos();
            //switch direction every 4 platforms
            if (i % 4 == 0)
            {
                RandomSpawnDirection();
            }
            //10th platform is a checkpoint
            if (i % 10 == 0)
            {
                //spawn checkpoint
                Instantiate(checkpoint, placementPos, transform.rotation);
            }
            else
            {
                SpawnRandomPlatform(placementPos);
            }
        }
        //spawn goal
        Instantiate(goal, placementPos, transform.rotation);
    }

    //needs current pos
    //finds new spawn location and places random platform
    private void SpawnRandomPlatform(Vector3 pos)
    {
        //save new platform and instantiate
        recentPlatform = platformList[Random.Range(0, platformList.Length)];
        Instantiate(recentPlatform, placementPos, transform.rotation);

    }

    private void nextSpawnPos()
    {
        placementPos.y += platformRising;
        switch (newDirection)
        {
            //up
            case 0:
                placementPos.z += platformSpacing;
                break;
            //right
            case 1:
                placementPos.x += platformSpacing;
                break;
            //down
            case 2:
                placementPos.z -= platformSpacing;
                break;
            //left
            case 3:
                placementPos.x -= platformSpacing;
                break;
        }
    }

    private void RandomSpawnDirection()
    {
        //rotate left or right for next platform
        oldDirection = newDirection;
        newDirection = Random.Range(oldDirection - 1, oldDirection + 2);
        //keep within 0-3
        newDirection += 4;
        newDirection %= 4;
    }
}
