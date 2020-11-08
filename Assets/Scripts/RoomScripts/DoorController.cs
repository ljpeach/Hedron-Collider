using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    bool inWay;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }
    void OnTriggerStay()
    {
        if (Input.GetButton("Interact"))
        {
            Debug.Log("dooropen");
            StartCoroutine("openDoor");
            inWay = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (inWay && other.gameObject.tag == "player")
        {
            inWay = false;
        }
    }

    IEnumerator openDoor()
    {
        while (transform.position.y > startPos.y - 5f)
        {
            Debug.Log(transform.position.y);
            transform.position -= new Vector3(0, .01f, 0);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        while(transform.position.y < startPos.y)
        {
            transform.position += new Vector3(0, .01f, 0);
            yield return null;
        }
        transform.position = startPos;
        yield break;
    }
}
