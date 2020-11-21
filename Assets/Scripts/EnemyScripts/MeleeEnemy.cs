using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int healthMax;
    public float speed;
    
    public float gravity;
    public float attackRange;
    public float chargeTime;
    public float postLockTime;
    public float chargeDist;

    public Material orig;
    public Material damageStay;
    public Material inv;
    public int faction;

    GameObject player;
    public GameObject aggro;
    Renderer rm;
    MainRoom parentRoom;
    Rigidbody rigidBody;
    Light lght;
    Vector3 target;
    int currentHealth;
    float airSpeed;
    bool isGrounded;
    int mode;
    float chargeDuration;
    float intensity;
    bool collided;
    bool aggroUpdated = false;
    Spawn aiCheck;
    bool dead = false;
    bool damageable = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<MiscReferences>().player;
        currentHealth = healthMax;
        airSpeed = 0;
        isGrounded = false;
        mode = 0;
        lght = GetComponent<Light>();
        rigidBody = GetComponent<Rigidbody>();
        intensity = 0;
        chargeDuration = 0;
        parentRoom = GetComponentInParent<MainRoom>();
        parentRoom.enemyCount += 1;
        collided = false;
        aiCheck = GetComponentInParent<Spawn>();
        rm = GetComponent<Renderer>();
        rm.material = orig;
        aggro = player;
    }

    void Update()
    {
        if (!aiCheck.aiOn)
        {
            if (damageable)
            {
                rm.material = inv;
            }
            mode = -1;
            transform.localScale = new Vector3(1f, 1f, 1f);
            lght.intensity = 0;
            chargeDuration = 0;
            gameObject.tag = "meleeEnemy";
            airSpeed = 0;
            damageable = false;
            return;
        }
        if (aggro == null)
        {
            aggro = player;
        }
        if (isGrounded || mode>=2)
        {
            airSpeed = 0;
        }
        else
        {
            airSpeed += -1 * gravity*Time.deltaTime;
        }
        if (mode == -1)
        {
            rm.material = orig;
            damageable = true;
            mode++;
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
        if (Vector3.Distance(transform.position, aggro.transform.position) <= attackRange)
        {
            mode += 1;
            return;
        }
        transform.LookAt(aggro.transform);
        Vector3 mid = Vector3.MoveTowards(transform.position, aggro.transform.position, speed * Time.deltaTime);
        mid.y += airSpeed * Time.deltaTime;
        transform.position = mid;
    }

    void charge()
    {
        if (chargeTime - chargeDuration > postLockTime)
        {
            transform.LookAt(aggro.transform);
            target = aggro.transform.position+transform.forward;
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
            aggroUpdated = false;
        }
        
    }

    void release()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, chargeDist * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 1 || collided)
        {
            mode = 0;
            gameObject.tag = "meleeEnemy";

            if (parentRoom.roomState == "Warring" && !aggroUpdated)
            {
                changeAggro();
            }
            else if (!aggroUpdated)
            {
                aggro = player;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemyDamage") && damageable)
        {
            StartCoroutine("showDamaged");
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            if (!dead && currentHealth <= 0)
            {
                dead = true;
                destroySequence();

            }
        }
        else if (other.gameObject.tag == "playerDamage" && parentRoom.roomState == "Warring" && damageable)
        {
            StartCoroutine("showDamaged");
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            if (!dead && currentHealth <= 0)
            {
                dead = true;
                destroySequence();

            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            collided = true;
        }
        else if (other.gameObject.tag == "playerDamage" && parentRoom.roomState == "Warring")
        {
            StartCoroutine("showDamaged");
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            if (!dead && currentHealth <= 0)
            {
                dead = true;
                destroySequence();

            }
        }
        else  if(other.gameObject.tag!="meleeEnemy")
        {
            collided = true;
        }


    }

    void OnCollisionExit(Collision other)
    {
        if( other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        collided = false;
    }

    public void destroySequence()
    {
        parentRoom.enemyCount--;
        if (parentRoom.enemyCount == 0)
        {
            parentRoom.cancelSwitch();
            parentRoom.emptySwitch();
        }
        Destroy(gameObject);
    }

    void changeAggro()
    {
        
        GameObject enemies = GetComponentInParent<Spawn>().gameObject;
        MainRanged[] rangedEnems = enemies.GetComponentsInChildren<MainRanged>();
        MeleeEnemy[] meleeEnems = enemies.GetComponentsInChildren<MeleeEnemy>();
        for (int i = 0; i < rangedEnems.Length; i++)
        {
            if (rangedEnems[i].faction != faction && Random.value <0.3f)
            {
                aggro = rangedEnems[i].gameObject;
                aggroUpdated = true;
            }
        }
        for (int i = 0; i < meleeEnems.Length; i++)
        {
            if (meleeEnems[i].faction != faction && Random.value < 0.3f)
            {
                aggro = meleeEnems[i].gameObject;
                aggroUpdated = true;
            }
        }
        if (!aggroUpdated)
        {
            aggro = player;
        }
    }

    IEnumerator showDamaged()
    {
        rm.material = damageStay;
        yield return new WaitForSeconds(.1f);
        rm.material = orig;
        yield break;
    }

}
