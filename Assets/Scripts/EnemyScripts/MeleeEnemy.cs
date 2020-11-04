using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int healthMax;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthMax;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("here1");
        if (other.gameObject.CompareTag("enemyDamage"))
        {
            Debug.Log("here");
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
        }
        if (currentHealth <= 0)
        {
            destroySequence();
        }
    }

    void destroySequence()
    {
        Destroy(gameObject);
    }

}
