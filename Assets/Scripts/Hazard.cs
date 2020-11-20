using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public bool destroyAfter = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (destroyAfter)
            {
                Invoke("selfDestruct", 0.5f);
            }
            else
            {
                StartCoroutine("tempDisable");
            }
        }
    }

    void selfDestruct()
    {
        Destroy(gameObject);
    }

    IEnumerator tempDisable()
    {
        MeshCollider mesh = GetComponent<MeshCollider>();
        mesh.enabled = false;
        yield return new WaitForSeconds(.3f);
        mesh.enabled = true;
        gameObject.tag = "playerDamage";
        yield break;
    }
}
