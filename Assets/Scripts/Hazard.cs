using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("selfDestruct", 0.5f);
        }
    }

    void selfDestruct()
    {
        Destroy(gameObject);
    }
}
