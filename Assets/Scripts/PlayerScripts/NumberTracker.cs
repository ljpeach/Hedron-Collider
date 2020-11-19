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
    public GameObject deathScreen;

    float currentHealth;
    void Start()
    {
        currentHealth = healthMax;
        setCountText();
        regen = true;
        regenRate += DifficultySetting.difficulty * 2.5f;
        respawner.transform.position = transform.position;
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
            setCountText();
        }
        if (currentHealth <= 0)
        {
            other.gameObject.GetComponentInParent<Spawn>().aiOn = false;
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
        if (other.gameObject.CompareTag("playerDamage"))
        {
            CancelInvoke();
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            other.gameObject.tag = "Untagged";
            regen = false;
            Invoke("regenOn", regenDelay);
            setCountText();
            if (currentHealth <= 0)
            {
                other.gameObject.GetComponentInParent<Spawn>().aiOn = false;
                destroySequence();
                setCountText();
            }
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
        deathScreen.gameObject.SetActive(true);
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        heal(healthMax);
    }

    public void heal(int healNum)
    {
        currentHealth = healNum;
    }
}
