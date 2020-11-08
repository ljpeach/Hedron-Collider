using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject aggro;
    //public ScalingTracker enemyScaler;

    meleeStats meleeNums;

    // Start is called before the first frame update
    void Awake()
    {
        aggro = GetComponentInParent<MiscReferences>().player;
        meleeNums = GetComponentInParent<meleeStats>();
        //enemyScaler = GetComponentInParent<ScalingTracker>();
    }
    public void createEnemies()
    {
        
        GameObject newEnemy = Instantiate(enemy, transform.position+new Vector3(0,1f,0), transform.rotation, GetComponentInParent<Spawn>().gameObject.transform);
        MeleeEnemy setup = newEnemy.GetComponent<MeleeEnemy>();
        setup.healthMax = meleeNums.enemyHealth;//10
        setup.speed = meleeNums.enemySpeed;//2
        setup.player = aggro;
        setup.gravity = meleeNums.enemyGravity;//20
        setup.attackRange = meleeNums.attackRange;//5
        setup.chargeTime = meleeNums.chargeTime;//5
        setup.postLockTime = meleeNums.postLockTime;//2
        setup.chargeDist = meleeNums.chargeDist;//10

        DealDamage dmgSetup = newEnemy.GetComponent<DealDamage>();
        dmgSetup.damage = meleeNums.enemyDamage;//5
    }
}
