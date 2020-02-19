using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public Vector3 playerSpawnPos;
    public GameObject title;
    public GameObject controls;

    private float speed = 15;
    public float xInput;
    public float zInput;

    private float jumpForce = 500;
    private int jumpMax = 2;
    private int jumpCount = 0;
    public GameObject playerCamera;
    private PlatformManager platformManagerScript;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        title.SetActive(false);
        controls.SetActive(false);
        platformManagerScript = GameObject.Find("PlatformManager").GetComponent<PlatformManager>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        //movement
        transform.Translate(Vector3.right * xInput * Time.deltaTime * speed, Space.Self);
        transform.Translate(Vector3.forward * zInput * Time.deltaTime * speed, Space.Self);


        if ((Input.GetKeyDown("joystick button 5") || Input.GetKeyDown("space")) && jumpCount > 0) {
            //cancel fall speed
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0.0f, playerRb.velocity.z);
            //add jump force
            playerRb.AddForce(Vector3.up * Time.deltaTime * jumpForce, ForceMode.Impulse);
            jumpCount -= 1;
        }

        //respawn on fall
        if (transform.position.y < playerSpawnPos.y -10)
        {
            transform.position = playerSpawnPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchFloor();
        }
        else if (collision.gameObject.CompareTag("Sticky"))
        {
            touchFloor();
            transform.parent = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            touchFloor();

            //get platform pos
            playerSpawnPos = collision.gameObject.transform.position;
            //raise spawn pos
            playerSpawnPos = new Vector3(playerSpawnPos.x, playerSpawnPos.y + 2.0f, playerSpawnPos.z);
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            //call to gamemanager to giv victor placement
            playerSpawnPos = platformManagerScript.PedestalData();
            transform.position = playerSpawnPos;
            //face toward losers
            if(platformManagerScript.CheckWinners() < 4)
            {
                transform.Rotate(new Vector3(0, 180, 0));
            }
            //face toward victors
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
        if(jumpCount == jumpMax)
        {
            jumpCount -= 1;
        }
    }

    //happens on all floor touches
    private void touchFloor()
    {
        jumpCount = jumpMax;
    }
}
