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
    public float sprintMulti;
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
#if UNITY_WEBGL
        // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
        lookMultiplier *= 0.25f;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        handleCameraRotate();
        handleMovement();
        //Debug.Log(transform.rotation);
    }

    void handleCameraRotate() 
    {
        //horizontal look
        transform.Rotate(new Vector3(0f,Input.GetAxisRaw("Mouse X")*lookMultiplier,0f));
        //vertical look
        float mouseData = -1*Input.GetAxisRaw("Mouse Y");
        float resetCam = camRotation;
        camRotation += -1*mouseData * lookMultiplier;
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
        float runMulti;
        if (Input.GetButton("Sprint"))
        {
            runMulti = sprintMulti;
        }
        else
        {
            runMulti = 1;
        }
        characterController.Move(transform.TransformVector(rawMove)*Time.deltaTime*moveSpeed*runMulti);
    }
}
