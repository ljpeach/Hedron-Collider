using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int healthMax;
    public float speed;
    public GameObject player;
    public float gravity;
    public float attackRange;
    public float chargeTime;
    public float postLockTime;
    public float chargeDist;

    Rigidbody rigidBody;
    Light lght;
    Transform playerPos;
    Vector3 target;
    int currentHealth;
    float airSpeed;
    bool isGrounded;
    int mode;
    float chargeDuration;
    float intensity;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = player.GetComponent<Transform>();
        currentHealth = healthMax;
        airSpeed = 0;
        isGrounded = false;
        mode = 0;
        lght = GetComponent<Light>();
        rigidBody = GetComponent<Rigidbody>();
        intensity = 0;
        chargeDuration = 0;
        //rn.material.EnableKeyword("_EmissiveExposureWeight");
    }

    void Update()
    {
        if (isGrounded || mode>=2)
        {
            airSpeed = 0;
        }
        else
        {
            airSpeed += -1 * gravity*Time.deltaTime;
        }
        if (mode == 0)
        {
            chase();
        }
        else if (mode == 1)
        {
            charge();
        }
        else if (mode == 2)
        {
            release();
        }
    }

    void chase()
    {
        if (Vector3.Distance(transform.position, playerPos.position) <= attackRange)
        {
            mode += 1;
            return;
        }
        transform.LookAt(playerPos);
        Vector3 mid = Vector3.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        mid.y += airSpeed * Time.deltaTime;
        transform.position = mid;
    }

    void charge()
    {
        if (chargeTime - chargeDuration > postLockTime)
        {
            transform.LookAt(playerPos);
            target = playerPos.position;
        }
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        chargeDuration += Time.deltaTime;
        Vector3 scale =new Vector3(1,1f-0.5f*(chargeDuration / chargeTime),1);
        intensity = 2f * chargeDuration / chargeTime;
        lght.intensity = intensity;
        transform.localScale = scale;
        if (scale.y <= 0.5f)
        {
            mode += 1;
            transform.localScale = new Vector3(1f, 1f, 1f);
            lght.intensity = 0;
            chargeDuration = 0;
            gameObject.tag = "playerDamage";
        }
        
    }

    void release()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, chargeDist * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 1)
        {
            mode = 0;
            gameObject.tag = "Untagged";
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemyDamage"))
        {
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            if (currentHealth <= 0)
            {
                //Debug.Log(currentHealth);
                destroySequence();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if( other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void destroySequence()
    {
        Destroy(gameObject);
    }

}
