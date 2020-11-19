using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 travelVector;
    float speed;
    float lifetime;
    //DealDamage damg;
    // Start is called before the first frame update
    void Start()
    {
        travelVector = transform.forward* speed;
        Invoke("DestroyProjectile", lifetime);
    }
    public void setAttributes(float spd, int dmg, float life)
    {
        
        speed = spd;
        GetComponent<DealDamage>().damage=dmg;
        //damg.setDamage(dmg);
        lifetime = life;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += travelVector*Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("ProjEnter");
        if (other.gameObject.tag != "AISphere" && other.gameObject.tag != "HealStation" && other.gameObject.tag != "Door" && !(other.gameObject.tag=="Player" && gameObject.tag=="enemyDamage"))
        { 
            DestroyProjectile(); 
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
