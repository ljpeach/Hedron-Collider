using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoom : MonoBehaviour
{
    public MainRoom[] neighborList;
    //public GameObject neighborList1, neighborList2, neighborList3, neighborList4, neighborList5, neighborList6;
    public GameObject enemySpawnManager;
    public string roomState;
    public float empty2Warring;
    public float warringDuration;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        /*
        neighborList[0] = neighborList1.GetComponent<MainRoom>();
        neighborList[1] = neighborList2.GetComponent<MainRoom>();
        neighborList[2] = neighborList3.GetComponent<MainRoom>();
        neighborList[3] = neighborList4.GetComponent<MainRoom>();
        neighborList[4] = neighborList5.GetComponent<MainRoom>();
        neighborList[5] = neighborList6.GetComponent<MainRoom>();
        */
        enemySpawnManager.GetComponent<Spawn>().fillOut();
    }

    void emptySwitch()
    {
        GetComponentInParent<ScalingTracker>().claimedCount--;
        roomState = "Empty";
        for (int i = 0; i < neighborList.Length; i++)
        {
            if (neighborList[i].GetComponent<MainRoom>().roomState == "Claimed")
            {
                Invoke("warringSwitch", empty2Warring);
            }
            else
            {
                CancelInvoke();
            }
        }
    }

    void warringSwitch()
    {
        Invoke("claimedSwitch", warringDuration);
    }

    void claimedSwitch()
    {
        enemySpawnManager.GetComponent<Spawn>().fillOut();
        GetComponentInParent<ScalingTracker>().claimedCount++;
        for (int i = 0; i < neighborList.Length; i++)
        {
            if (neighborList[i].GetComponent<MainRoom>().roomState == "Empty")
            {
                neighborList[i].GetComponent<MainRoom>().warringSwitch();
            }
        }
    }
}
