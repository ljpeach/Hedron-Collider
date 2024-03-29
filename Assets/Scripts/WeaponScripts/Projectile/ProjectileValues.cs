﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileValues : MonoBehaviour
{
    public GameObject rotationY;
    public GameObject rotationX;
    Transform trY;
    Transform trX;
    Transform start;
    public float speed;
    public int damage;
    public GameObject projectile;
    public float projectileLife;
    public string damageTag;
    public Transform parentLoc;
    

    void Start()
    {
        trY = rotationY.GetComponent<Transform>();
        trX = rotationX.GetComponent<Transform>();
        start = GetComponent<Transform>();
    }

    public void Shoot()
    {
        GameObject proj=Instantiate(projectile,start.position,start.rotation,parentLoc);
        proj.transform.localScale = new Vector3(1f, 1f, 1f);
        proj.tag = damageTag;
        Transform projTransform = proj.GetComponent<Transform>();
        Projectile projInfo = proj.GetComponent<Projectile>();
        projInfo.setAttributes(speed, damage,projectileLife);
    }
}
