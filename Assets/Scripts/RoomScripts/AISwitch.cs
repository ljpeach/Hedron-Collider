using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AISwitch : MonoBehaviour
{
    public GameObject enemyBox;
    public GameObject doorText;
    GameObject campUI;

    Spawn enemyConditions;

    void Start()
    {
         enemyConditions= enemyBox.GetComponent<Spawn>();
        campUI = GetComponentInParent<MiscReferences>().campUI;
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
        if (!doorText.activeSelf && other.gameObject.tag=="Player" && parentRoom.roomState=="Empty" && playerNums.campCount>=1)
        {
            campUI.SetActive(true);
            campUI.GetComponent<TextMeshProUGUI>().text = "Press E to Place A Camp.";
            if (Input.GetButton("Interact"))
            {
                campUI.SetActive(false);
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
            campUI.SetActive(false);
        }
    }
}
