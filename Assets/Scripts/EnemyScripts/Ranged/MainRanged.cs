using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRanged : MonoBehaviour
{ 
    public float RoF;
    public float radius = 5;
    public float turnSpeed;
    public float healthMax;
    public float height;
    public int direction;
    public Material orig;
    public Material damageStay;

    Renderer rm;
    GameObject player;
    MainRoom parentRoom;
    float currentHealth;
    float speed;
    float angle = 0;
    float timer;
    ProjectileValues gun;
    Vector3 center;
    Spawn aiCheck;
    Light lght;
    float intensity;
    bool dead = false;

    void Start()
    {
        player = GetComponentInParent<MiscReferences>().player;
        parentRoom = GetComponentInParent<MainRoom>();
        parentRoom.enemyCount += 1;
        aiCheck = GetComponentInParent<Spawn>();
        lght = GetComponentInChildren<Light>();
        intensity = 0;
        direction = 1;
        speed = (2 * Mathf.PI) / turnSpeed;
        timer = 0;
        gun = GetComponentInChildren<ProjectileValues>();
        center = player.transform.position;
        currentHealth = healthMax;
        GetComponentInChildren<ProjectileValues>().parentLoc = GetComponentInParent<Spawn>().transform;
        rm = transform.Find("Geometry").Find("Tetrahedron").gameObject.GetComponent<Renderer>();
        rm.material = orig;
    }

    // Update is called once per frame
    void Update()
    {
        if (!aiCheck.aiOn)
        {
            lght.intensity = 0;
            timer = 0;
            return;
        }
        if (Mathf.Pow(player.transform.position.x - center.x, 2) + Mathf.Pow(player.transform.position.z - center.z, 2) > radius*radius)
        {
            direction *= -1;
            updateCenter();
        }
        transform.LookAt(player.transform.position);

        intensity = 0.5f * timer / RoF;
        lght.intensity = intensity;

        if (timer >= RoF)
        {
            gun.Shoot();
            timer = 0;
        }
        timer += Time.deltaTime;
        angle += speed * Time.deltaTime *direction;
        Vector3 target = new Vector3(Mathf.Cos(angle) * radius + center.x, height, Mathf.Sin(angle) * radius + center.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed*Time.deltaTime*5);
        
    }

    void OnCollisionEnter()
    {
        direction *= -1;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyDamage")
        {
            StartCoroutine("showDamaged");
            currentHealth -= other.gameObject.GetComponent<DealDamage>().damage;
            if (currentHealth <= 0 && !dead)
            {
                destroySequence();
            }
        }
    }

    public void destroySequence()
    {
        parentRoom.enemyCount--;
        if (parentRoom.enemyCount == 0)
        {
            parentRoom.emptySwitch();
        }
        Destroy(gameObject);
    }

    void updateCenter()
    {
        center = player.GetComponent<Transform>().position;
    }

    IEnumerator showDamaged()
    {
        rm.material = damageStay;
        yield return new WaitForSeconds(.1f);
        rm.material = orig;
        yield break;
    }
}