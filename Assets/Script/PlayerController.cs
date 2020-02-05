﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public Vector3 playerSpawnPos;

    private float speed = 15;
    public float xInput;
    public float zInput;

    private float jumpForce = 500;
    private int jumpCount = 0;
    public GameObject playerCamera;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
            //cancel fall speed then jump
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0.0f, playerRb.velocity.z);
            playerRb.AddForce(Vector3.up * Time.deltaTime * jumpForce, ForceMode.Impulse);
            jumpCount -= 1;
        }

        if(transform.position.y < -10)
        {
            transform.position = playerSpawnPos;
        }
    }

    //double jump for control

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 2;
        }
        else if (collision.gameObject.CompareTag("Sticky"))
        {
            jumpCount = 2;
            transform.parent = collision.gameObject.transform;
        }
        else if (collision.gameObject.CompareTag("Goal"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Checkpoint"))
        {
            jumpCount = 2;
            playerSpawnPos = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        transform.parent = null;
        if(jumpCount == 2)
        {
            jumpCount -= 1;
        }
    }
}
