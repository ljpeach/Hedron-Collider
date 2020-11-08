using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoom : MonoBehaviour
{
    public GameObject[] neighborList;
    public GameObject camp;
    //public GameObject neighborList1, neighborList2, neighborList3, neighborList4, neighborList5, neighborList6;
    public GameObject enemySpawnManager;
    public string roomState;
    public float empty2Warring;
    public float warringDuration;
    public int enemyCount;
    ScalingTracker claimCounter;
    
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
        claimCounter = GetComponentInParent<ScalingTracker>();
        if (roomState == "Claimed")
        {
            claimCounter.claimedCount++;
            enemySpawnManager.GetComponent<Spawn>().fillOut();
        }
    }

    public void emptySwitch()
    {
        claimCounter.claimedCount--;
        roomState = "Empty";

        for (int i = 0; i < neighborList.Length; i++)
        {
            MainRoom roomRef = neighborList[i].GetComponent<MainRoom>();
            if (roomRef.roomState == "Claimed")
            {
                invokeWar();
            }
            bool cancel = true;
            for(int j=0; j<roomRef.neighborList.Length; j++)
            {
                if (roomRef.neighborList[j].GetComponent<MainRoom>().roomState == "Claimed")
                {
                    cancel = false;
                    break;
                }
            }
            if (roomRef.roomState=="Empty" && cancel) 
            {
                roomRef.cancelSwitch();
            }
        }
    }

    public void campSwitch()
    {
        roomState = "Camp";
        cancelSwitch();
        Transform parent = GetComponent<Transform>();
        foreach (Transform child in parent)
        {
            if (child.name == "RoomCenter")
            {
                Instantiate(camp, child);
                GetComponentInParent<MiscReferences>().player.GetComponent<NumberTracker>().respawner.transform.position = child.position;
                break;
            }
        }
    }

    void warringSwitch()
    {
        roomState = "Warring";
        Invoke("claimedSwitch", warringDuration);
    }

    void claimedSwitch()
    {
        roomState = "Claimed";
        enemySpawnManager.GetComponent<Spawn>().fillOut();
        claimCounter.claimedCount++;
        for (int i = 0; i < neighborList.Length; i++)
        {
            if (neighborList[i].GetComponent<MainRoom>().roomState == "Empty")
            {
                neighborList[i].GetComponent<MainRoom>().invokeWar();
            }
        }
    }

    public void invokeWar()
    {
        Invoke("warringSwitch", empty2Warring);
    }

    public void cancelSwitch()
    {
        CancelInvoke();
    }
}
