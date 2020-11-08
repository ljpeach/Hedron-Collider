﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool aiOn;
    void awake()
    {
        aiOn = false;
    }
    public void fillOut()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.BroadcastMessage("createEnemies", SendMessageOptions.DontRequireReceiver);
        }
    }
}