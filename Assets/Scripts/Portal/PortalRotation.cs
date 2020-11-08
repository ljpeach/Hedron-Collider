using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{
    public GameObject player;
    Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerPos);
        transform.eulerAngles =new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
