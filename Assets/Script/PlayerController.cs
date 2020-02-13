﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public Vector3 playerSpawnPos;
    public GameObject title;
    public GameObject countdown;

    private float speed = 15;
    public float xInput;
    public float zInput;

    private float jumpForce = 500;
    private int jumpMax = 2;
    private int jumpCount = 0;
    public GameObject playerCamera;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        title = GameObject.Find("Title");
        title.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        //set yPos of camera to 0
        Vector3 playerRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z);
        Vector3 playerForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z);

        //move in relation to camera
        transform.Translate(playerRight * xInput * Time.deltaTime * speed);
        transform.Translate(playerForward * zInput * Time.deltaTime * speed);


        if ((Input.GetKeyDown("joystick button 5") || Input.GetKeyDown("space")) && jumpCount > 0) {
            //cancel fall speed
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0.0f, playerRb.velocity.z);
            //add jump force
            playerRb.AddForce(Vector3.up * Time.deltaTime * jumpForce, ForceMode.Impulse);
            jumpCount -= 1;
        }

        if(transform.position.y < -10)
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
            //place on pedistal
            Destroy(gameObject);
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
