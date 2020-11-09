using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealStation : MonoBehaviour
{
    GameObject campUI;
    void Start()
    {
        campUI = GetComponentInParent<MiscReferences>().campUI;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            campUI.SetActive(true);
            campUI.GetComponent<TextMeshProUGUI>().text = "Press E to Heal.";
            if (Input.GetButton("Interact"))
            {
                //Debug.Log("pressed");
                NumberTracker playerNums = other.gameObject.GetComponent<NumberTracker>();
                playerNums.heal(playerNums.healthMax);
                AmmoTracker ammo = playerNums.GetComponentInChildren<AmmoTracker>();
                ammo.reload();
            }
        }
    }

    void OnTriggerExit()
    {
        campUI.SetActive(false);
    }

}
