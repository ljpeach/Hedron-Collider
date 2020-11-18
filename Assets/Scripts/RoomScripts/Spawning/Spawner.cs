using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject[] enemies;
    meleeStats meleeNums;
    RangedStats rangedNums;

    int choice;

    // Start is called before the first frame update
    void Awake()
    {
        meleeNums = GetComponentInParent<meleeStats>();
        rangedNums = GetComponentInParent<RangedStats>();
        //enemyScaler = GetComponentInParent<ScalingTracker>();
        enemies = GetComponentInParent<EnemyList>().enemyList;
        choice = Random.Range(0, enemies.Length);
    }
    public void createEnemies(int faction)
    {

        GameObject newEnemy = Instantiate(enemies[choice], transform.position + new Vector3(0, 1f, 0), transform.rotation, GetComponentInParent<Spawn>().gameObject.transform);
        if (choice == 0)
        {
            MeleeEnemy setup = newEnemy.GetComponent<MeleeEnemy>();
            setup.healthMax = meleeNums.enemyHealth;//10
            setup.speed = meleeNums.enemySpeed;//2
            setup.gravity = meleeNums.enemyGravity;//20
            setup.attackRange = meleeNums.attackRange;//5
            setup.chargeTime = meleeNums.chargeTime;//5
            setup.postLockTime = meleeNums.postLockTime;//2
            setup.chargeDist = meleeNums.chargeDist;//10
            setup.orig = GetComponentInParent<ScalingTracker>().factionMats[faction];
            setup.faction = faction;

            DealDamage dmgSetup = newEnemy.GetComponent<DealDamage>();
            dmgSetup.damage = meleeNums.enemyDamage;//5
        }
        else
        {
            MainRanged setup = newEnemy.GetComponent<MainRanged>();
            setup.RoF = rangedNums.RoF;
            setup.radius = rangedNums.radius;
            setup.turnSpeed = rangedNums.turnSpeed;
            setup.healthMax = rangedNums.healthMax;
            setup.height = rangedNums.height;
            setup.orig = GetComponentInParent<ScalingTracker>().factionMats[faction];
            setup.faction = faction;

            ProjectileValues dmgSetup = newEnemy.GetComponentInChildren<ProjectileValues>();
            dmgSetup.damage = rangedNums.damage;//5
        }
    }
}
