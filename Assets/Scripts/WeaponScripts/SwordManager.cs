using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public int swordDamage;
    int animationState;
    Animation anim;
    string[] animNames ={"LeftSwipe","RightSwipe","ReturnSwipe"};

    void Start()
    {
        animationState = 0;
        anim = GetComponent<Animation>();
        
    }

    void Update()
    {
        if (Input.GetButton("Fire1") &&!anim.isPlaying)
        {
            CancelInvoke();
            anim.Play(animNames[animationState]);
            if (animationState == 2)
            {
                animationState = 1;
            }
            else
            { 
                animationState += 1;
            }
            if (animationState >= 3)
            {
                animationState = 0;
            }
            Invoke("reset", 1f);
        }
    }

    void reset()
    {
        anim.Play(animNames[animationState-1] + "Return");
        animationState = 0;
    }

}
