using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetAndDistance
{
    public GameObject t;
    public float d;

    public TargetAndDistance(GameObject target, float distance)
    {
        this.t = target;
        this.d = distance;
    }
}
