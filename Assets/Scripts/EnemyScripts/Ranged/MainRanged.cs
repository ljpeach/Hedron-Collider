using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRanged : MonoBehaviour
{
    public GameObject player;
    public float RoF;
    public float radius = 5;
    public float turnSpeed;
    public float healthMax;
    public float currentHealth;
    public int direction;

    float speed;
    float angle = 0;
    float timer;
    ProjectileValues gun;
    Vector3 center;
    float height;

    void Start()
    {
        direction = 1;
        speed = (2 * Mathf.PI) / turnSpeed;
        timer = 0;
        gun = GetComponentInChildren<ProjectileValues>();
        center = player.transform.position;
        height = transform.position.y;
        currentHealth = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Pow(player.transform.position.x - center.x, 2) + Mathf.Pow(player.transform.position.z - center.z, 2) > radius*radius)
        {
            direction *= -1;
            updateCenter();
        }
        transform.LookAt(player.transform.position);
        if (timer >= RoF)
        {
            gun.Shoot();
            timer = 0;
        }
        timer += Time.deltaTime;
        angle += speed * Time.deltaTime *direction;
        transform.position = new Vector3(Mathf.Cos(angle) * radius + center.x, height, Mathf.Sin(angle) * radius + center.z);
        
    }

    public void destroySequence()
    {
        Destroy(gameObject);
    }

    void updateCenter()
    {
        center = player.GetComponent<Transform>().position;
    }
}