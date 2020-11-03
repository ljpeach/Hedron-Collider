﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float cameraVerticalAngle;
    public float lookMultiplier;
    public Camera PlayerCam;
    public float moveSpeed;
    public float gravity;
    public float jumpHeight;
    float airSpeed;
    float camRotation;

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        camRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        handleCameraRotate();
        handleMovement();
    }

    void handleCameraRotate() 
    {
        //horizontal look
        transform.Rotate(new Vector3(0f,Input.GetAxisRaw("Mouse X")*lookMultiplier,0f));
        //vertical look
        float mouseData = -1*Input.GetAxisRaw("Mouse Y");
        float resetCam = camRotation;
        camRotation += -1*mouseData * lookMultiplier;
        Debug.Log(camRotation);
        if (camRotation >= -90 && camRotation <= 90)
        {
            PlayerCam.transform.Rotate(-1 * Input.GetAxisRaw("Mouse Y") * lookMultiplier, 0f, 0f);
        }
        else
        {
            camRotation = resetCam;
        }
    }

    void handleMovement()
    {
        Vector3 rawMove=new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
        if (!characterController.isGrounded)
        {
            airSpeed += -1*gravity*Time.deltaTime;
        }
        else
        {
            airSpeed = 0;
            if (Input.GetButton("Jump"))
            {
                airSpeed += jumpHeight;
            }
        }
        rawMove.y += airSpeed;
        characterController.Move(transform.TransformVector(rawMove)*Time.deltaTime*moveSpeed);
    }
}
