using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject aggro;
    // Start is called before the first frame update
    void Start()
    {
        aggro = GetComponentInParent<PlayerIndicator>().player;
        Debug.Log(aggro);
    }
    public void createEnemies()
    {
        
        GameObject newEnemy = Instantiate(enemy, transform.position+new Vector3(0,1f,0), transform.rotation);
        MeleeEnemy setup = newEnemy.GetComponent<MeleeEnemy>();
        setup.healthMax = 10;
        setup.speed = 2;
        setup.player = aggro;
        setup.gravity = 10;
        setup.attackRange = 5;
        setup.chargeTime = 5;
        setup.postLockTime = 2;
        setup.chargeDist = 10;

        DealDamage dmgSetup = newEnemy.GetComponent<DealDamage>();
        dmgSetup.damage = 5;
    }
}
