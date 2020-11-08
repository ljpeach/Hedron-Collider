using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTracker : MonoBehaviour
{
    public int ammoCount;
    public float rechargeRate;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI entireAmmo;

    float ammoMax;
    float currentAmmo;
    // Start is called before the first frame update
    void Start()
    {
        ammoMax = 100f;
        currentAmmo = ammoMax;
    }

    void Update()
    {
        if (currentAmmo < ammoMax)
        {
            currentAmmo += rechargeRate / 100;
        }
        else
        {
            currentAmmo = 100f;
        }
        setCountText();
    }

    void setCountText()
    {
        countText.text = "Ammo: " + ((int)(currentAmmo/(100f/ammoCount))).ToString() + "/" + (ammoCount).ToString();
        entireAmmo.text = currentAmmo.ToString() + "/100";
    }

    public void removeAmmo()
    {
        currentAmmo -= 100 / ammoCount;
    }

    public bool canFire()
    {
        return (100 / ammoCount) <= currentAmmo;
    }

    public void reload()
    {
        currentAmmo = ammoMax;
    }
}
