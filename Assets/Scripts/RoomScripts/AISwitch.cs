using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISwitch : MonoBehaviour
{
    public GameObject enemyBox;

    Spawn enemyConditions;

    void Start()
    {
         enemyConditions= enemyBox.GetComponent<Spawn>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyConditions.aiOn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyConditions.aiOn = false;
        }
    }
}
