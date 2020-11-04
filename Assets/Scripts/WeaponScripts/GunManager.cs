using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager: MonoBehaviour
{
    Animation anim;
    ProjectileValues shooty;
    
    public GameObject projectileSource;
    public GameObject weaponHandler;
    AmmoTracker ammo;


    // Start is called before the first frame update
    void Start()
    {
        ammo=weaponHandler.GetComponent<AmmoTracker>();
        anim = GetComponent<Animation>();
        shooty = projectileSource.GetComponent<ProjectileValues>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !anim.isPlaying && ammo.canFire())
        {
            anim.Play("Recoil");
            shooty.Shoot();
            ammo.removeAmmo();
        }
    }
}
