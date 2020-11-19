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
    public int faction;
    public bool warStart = false;
    ScalingTracker claimCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        claimCounter = GetComponentInParent<ScalingTracker>();
        if (roomState == "Claimed")
        {
            if (faction == -1)
            {
                faction = Random.Range(0, claimCounter.factionList.Length);
            }
            claimCounter.factionList[faction]++;
            enemySpawnManager.GetComponent<Spawn>().fillOut();
        }
    }

    public void emptySwitch()
    {
        claimCounter.factionList[faction]--;
        roomState = "Empty";
        warStart = false;
        for (int i = 0; i < neighborList.Length; i++)
        {
            MainRoom roomRef = neighborList[i].GetComponent<MainRoom>();
            if (roomRef.roomState == "Claimed")
            {
                invokeWar();
                break;
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
        enemySpawnManager.GetComponent<Spawn>().warFillOut();
        Invoke("claimedSwitch", warringDuration);
    }

    void claimedSwitch()
    {
        roomState = "Claimed";
        if (enemyCount>0)
        {
            MainRanged[] rangedEnemies = GetComponentsInChildren<MainRanged>();
            for (int i=0; i<rangedEnemies.Length; i++)
            {
                if (rangedEnemies[i].faction != faction)
                {
                    rangedEnemies[i].destroySequence();
                }
            }
            MeleeEnemy[] meleeEnemies = GetComponentsInChildren<MeleeEnemy>();
            for (int i=0; i<meleeEnemies.Length; i++)
            {
                if (meleeEnemies[i].faction != faction)
                {
                    meleeEnemies[i].destroySequence();
                }
            }
        }
        enemySpawnManager.GetComponent<Spawn>().fillOut();
        claimCounter.factionList[faction]++;
        for (int i = 0; i < neighborList.Length; i++)
        {
            MainRoom neighbor = neighborList[i].GetComponent<MainRoom>();
            if (neighbor.roomState == "Empty" && !neighbor.warStart)
            {
                neighbor.invokeWar();
            }
        }
    }

    public void invokeWar()
    {
        warStart = true;
        faction = victorCalculate();
        Invoke("warringSwitch", empty2Warring);
    }

    public void cancelSwitch()
    {
        CancelInvoke();
    }

    int victorCalculate()
    {
        int[] factionTracker = new int[claimCounter.factionList.Length];
        for (int i = 0; i < neighborList.Length; i++)
        {
            if (neighborList[i].GetComponent<MainRoom>().roomState == "Claimed")
            {
                factionTracker[neighborList[i].GetComponent<MainRoom>().faction]+=1;
            }
        }
        int newFact = 0;
        float maxScore = 0;
        for (int i = 0; i < factionTracker.Length; i++)
        {
            float factScore = factionTracker[i] * claimCounter.factionList[i]+Random.value;
            if (factScore>maxScore)
            {
                maxScore = factScore;
                newFact = i;
            }
        }
        return newFact;
    }
}
