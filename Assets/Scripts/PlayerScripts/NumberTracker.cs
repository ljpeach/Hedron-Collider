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
    public bool regen;
    public float regenRate;
    public float regenDelay;

    float currentHealth;
    void Start()
    {
        currentHealth = healthMax;
        setCountText();
        regen = true;
    }

    void Update()
    {
        if (regen && currentHealth < healthMax)
        {
            currentHealth += regenRate * Time.deltaTime;
            setCountText();

        }
        else if (regen)
        {
            currentHealth = healthMax;
            setCountText();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("playerDamage"))
        {
            CancelInvoke();
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            setCountText();
            other.gameObject.tag = "Untagged";
            regen = false;
            Invoke("regenOn", regenDelay);
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

    void regenOn()
    {
        regen = true;
    }

    void destroySequence()
    {
        print("sad times...");
    }
}
