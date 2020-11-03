using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public int swordDamage;
    int animationState;
    Animation anim;
    string[] animNames ={"LeftSwipe","RightSwipe"};

    void Start()
    {
        animationState = 0;
        anim = GetComponent<Animation>();
        
    }

    void Update()
    {
        if (Input.GetButton("Fire1") &&!anim.isPlaying)
        {
            Debug.Log(anim["RightSwipe"].name);
            anim.Play(animNames[animationState]);
            animationState += 1;
            if (animationState >= animNames.Length)
            {
                animationState = 0;
            }
        }
    }

}
