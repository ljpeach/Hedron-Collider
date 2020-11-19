using System.Collections;
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
        Spawner[] spawners = GetComponentsInChildren<Spawner>();

        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].createEnemies(GetComponentInParent<MainRoom>().faction);
        }
    }

    public void warFillOut()
    {
        Spawner[] spawners = GetComponentsInChildren<Spawner>();
        GameObject[] rooms = GetComponentInParent<MainRoom>().neighborList;
        int j = 0;
        for (int i = 0; i < rooms.Length && j<spawners.Length; i++)
        {
            MainRoom neighbor = rooms[i].GetComponent<MainRoom>();
            if (neighbor.roomState == "Claimed")
            {
                spawners[j].createEnemies(neighbor.faction);
                j++;
            }
        }
    }

}
