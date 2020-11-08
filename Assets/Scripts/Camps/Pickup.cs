using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    float startHeight;
    float x;
    void Start()
    {
        startHeight = transform.position.y;
        x = 0;
    }
    void Update()
    {
        transform.Translate( new Vector3(0f, Mathf.Sin(x), 0f)*Time.deltaTime*.2f);
        x += Time.deltaTime;
        if (x >= Mathf.PI * 2)
        {
            x -= Mathf.PI * 2;
        }
        transform.Rotate(new Vector3(0, 90f, 0) * Time.deltaTime);
    }
}
