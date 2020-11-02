using System.Collections;
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

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
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
        PlayerCam.transform.Rotate(-1*Input.GetAxisRaw("Mouse Y")*lookMultiplier,0f,0f);
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
