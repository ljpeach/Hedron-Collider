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
    public GameObject respawner;

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
            other.gameObject.tag = "Untagged";
            regen = false;
            Invoke("regenOn", regenDelay);
            other.gameObject.GetComponentInParent<Spawn>().aiOn = false;
            setCountText();
        }
        if (currentHealth <= 0)
        {
            destroySequence();
            setCountText();
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("campPickup"))
        {
            campCount++;
            Destroy(other.gameObject);
        }
    }

    void setCountText()
    {
        healthBar.text = "Health: " + ((int)currentHealth).ToString() + "/" + (healthMax).ToString();
        campBar.text = "Camps:" + campCount.ToString();
    }

    void regenOn()
    {
        regen = true;
    }

    void destroySequence()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.enabled = false;
        transform.position = respawner.transform.position;
        characterController.enabled = true;
        heal(healthMax);
    }

    public void heal(int healNum)
    {
        currentHealth = healNum;
    }
}
