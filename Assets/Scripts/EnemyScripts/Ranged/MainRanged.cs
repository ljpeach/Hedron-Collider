using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRanged : MonoBehaviour
{
    public GameObject player;
    public float RoF;
    float timer;
    ProjectileValues gun;

    void Start()
    {
        timer = 0;
        gun = GetComponentInChildren<ProjectileValues>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        if (timer >= RoF)
        {
            gun.Shoot();
            timer = 0;
        }
        timer += Time.deltaTime;
    }
}
