using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingTracker : MonoBehaviour
{
    public int claimedCount;
    // Start is called before the first frame update
    public float scaleCalculator()
    { 
        return 1/claimedCount; 
    }
}
