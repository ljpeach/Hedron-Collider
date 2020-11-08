﻿using System.Collections;
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

    void OnTriggerStay(Collider other)
    {
        MainRoom parentRoom = GetComponentInParent<MainRoom>();
        NumberTracker playerNums = other.gameObject.GetComponent<NumberTracker>();
        if (other.gameObject.tag=="Player" && parentRoom.roomState=="Empty" && playerNums.campCount>=1)
        {
            Debug.Log("e to camp");
            if (Input.GetButton("Interact"))
            {
                parentRoom.campSwitch();
                playerNums.campCount--;
            }
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
