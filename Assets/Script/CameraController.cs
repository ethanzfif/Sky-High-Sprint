﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float cameraSpeed = 200.0f;
    private float cameraInput;
    // Start is called before the first frame update
    void Start()
    {
        //find a way to turn this off on quit
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/
    }

    // Update is called once per frame
    void Update()
    {
        //mouse
        if (Input.GetAxis("Mouse X") != 0)
        {
            cameraInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, cameraInput * cameraSpeed / 2 * Time.deltaTime);
        }
        //controller
        else
        {
            cameraInput = Input.GetAxis("Stick X");
            transform.Rotate(Vector3.up, cameraInput * cameraSpeed * Time.deltaTime);
        }

        //set input to zero to fight drifting
        cameraInput = 0.0f;
    }
}
