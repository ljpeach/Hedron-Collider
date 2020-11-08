using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealStation : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("e to heal");
            if (Input.GetButton("Interact"))
            {
                Debug.Log("pressed");
                NumberTracker playerNums = other.gameObject.GetComponent<NumberTracker>();
                playerNums.heal(playerNums.healthMax);
                AmmoTracker ammo = playerNums.GetComponentInChildren<AmmoTracker>();
                ammo.reload();
            }
        }
    }

}
