using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public Vector3 placementPos;
    public Quaternion placementRotation;
    //x or z distance
    private float platformSpacing = 15.0f;
    //y distance
    private float platformRising = 2.0f;
    //0 = up, 1 = right, 2 = down, 3 = left
    private int oldDirection = 0;
    private int newDirection = 0;
    public GameObject[] platformList;
    private int platformCount = 0; //50
    public GameObject checkpoint;
    public GameObject goal;
    public GameObject pedestal;
    private GameObject recentPlatform;
    public bool canSpawnPlatforms = false;
    private int winners = 0;
    private GameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        winners = 0;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canSpawnPlatforms)
        {
            SpawnPlatforms(platformCount);
            canSpawnPlatforms = false;
        }
    }

    //needs platformCount
    //spawns platforms
    public void SpawnPlatforms(int platformNumber)
    {
        //find spawn pos 1
        placementPos = transform.position;
        placementRotation = transform.rotation;
        //place platforms needed
        for (int i = 1; i < platformNumber; i++)
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
                Instantiate(checkpoint, placementPos, placementRotation);
            }
            else
            {
                SpawnRandomPlatform(placementPos);
            }
        }
        //spawn goal
        nextSpawnPos();
        Instantiate(goal, placementPos, placementRotation);
        //give space for pedestal
        for(int i = 0; i < 4; i++)
        {
            nextSpawnPos();
        }
        //spawn pedistal
        Instantiate(pedestal, placementPos, placementRotation);
    }

    //needs current pos
    //finds new spawn location and places random platform
    private void SpawnRandomPlatform(Vector3 pos)
    {
        //save new platform and instantiate
        recentPlatform = platformList[Random.Range(0, platformList.Length)];
        Instantiate(recentPlatform, placementPos, placementRotation);

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
        //rotate platform
        if (newDirection > oldDirection)
        {
            placementRotation *= Quaternion.Euler(0, 90, 0);
        }
        else if (newDirection < oldDirection)
        {
            placementRotation *= Quaternion.Euler(0, -90, 0);
        }
        //keep within 0-3
        newDirection += 4;
        newDirection %= 4;
    }

    //returns position on pedistal for victor
    public Vector3 PedestalData()
    {
        winners++;
        gameManagerScript.DisplayVictor(winners);
        switch (winners)
        {
            case 1:
                //first pedestal
                placementPos += new Vector3(0, 10, 0);
                return placementPos;
            case 2:
                //second pedestal

                //rotate left
                newDirection -= 1;
                //keep within 0-3
                newDirection += 4;
                newDirection %= 4;

                nextSpawnPos();
                placementPos -= new Vector3(0, 5, 0);
                return placementPos;
            case 3:
                //third pedestal

                //rotate 180
                newDirection += 2;
                //keep within 0-3
                newDirection += 4;
                newDirection %= 4;

                nextSpawnPos();
                nextSpawnPos();
                placementPos -= new Vector3(0, 10, 0);
                return placementPos;
            default:
                //loss pedestal

                //rotate 180
                newDirection += 2;
                //keep within 0-3
                newDirection += 4;
                newDirection %= 4;

                nextSpawnPos();

                //rotate left
                newDirection -= 1;
                //keep within 0-3
                newDirection += 4;
                newDirection %= 4;

                nextSpawnPos();
                nextSpawnPos();
                placementPos -= new Vector3(0, 5, 0);
                return placementPos;
        }
    }

    public int CheckWinners()
    {
        return winners;
    }
}
