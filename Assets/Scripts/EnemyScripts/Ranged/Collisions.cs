using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    MainRanged rangedBody;

    void Start()
    {
        rangedBody = GetComponentInParent<MainRanged>();
    }

    void OnCollision()
    {
        rangedBody.direction *= -1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyDamage")
        {
            rangedBody.currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            Debug.Log(rangedBody.currentHealth);
            if (rangedBody.currentHealth <= 0)
            {
                rangedBody.destroySequence();
            }
        }
    }
}
