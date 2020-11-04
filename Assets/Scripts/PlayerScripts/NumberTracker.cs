using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NumberTracker : MonoBehaviour
{
    public int campCount;
    public int healthMax;
    public TextMeshProUGUI healthBar;
    public TextMeshProUGUI campBar;

    int currentHealth;
    void Start()
    {
        currentHealth = healthMax;
        setCountText();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("playerDamage"))
        {
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            setCountText();
            other.gameObject.tag = "Untagged";
        }
        if (currentHealth <= 0)
        {
            destroySequence();
        }
    }

    void setCountText()
    {
        healthBar.text = "Health: " + currentHealth.ToString() + "/" + (healthMax).ToString();
        campBar.text = "Camps:" + campCount.ToString();
    }

    void destroySequence()
    {
        print("sad times...");
    }
}
