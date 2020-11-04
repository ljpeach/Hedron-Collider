using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 travelVector;
    float speed;
    int damage;
    float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        travelVector = transform.forward* speed;
        Invoke("DestroyProjectile", lifetime);
    }
    public void setAttributes(float spd, int dmg, float life)
    {
        speed = spd;
        damage = dmg;
        lifetime = life;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += travelVector*Time.deltaTime;
    }

    void OnCollision()
    {
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
