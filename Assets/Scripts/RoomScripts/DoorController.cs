using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour
{
    Vector3 startPos;
    GameObject e2open;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        e2open = GetComponentInParent<MiscReferences>().doorUI;
        e2open.SetActive(false);
    }
    void OnTriggerStay(Collider other)
    {
        if( other.gameObject.tag == "Player")
        {
            e2open.SetActive(true);
            if (Input.GetButton("Interact"))
            {
                //Debug.Log("dooropen");
                StartCoroutine("openDoor");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            e2open.SetActive(false);
        }
    }

    IEnumerator openDoor()
    {
        while (transform.position.y > startPos.y - 5f)
        {
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
