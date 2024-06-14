using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float bulletSpeed = 0;

    // Note that this vector must be of uniform length!
    public Vector3 unifBulletVector = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        transform.position += unifBulletVector * bulletSpeed * Time.deltaTime;
    }
}
