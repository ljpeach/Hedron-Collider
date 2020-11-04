using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject sword;
    public GameObject gun;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            sword.SetActive(false);
            gun.SetActive(true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            sword.SetActive(true);
            gun.SetActive(false);
        }
    }
}
