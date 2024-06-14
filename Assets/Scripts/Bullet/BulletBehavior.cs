using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float bulletSpeed;

    // Note that this vector must be of uniform length!
    private Vector3 unifBulletVector;

    public void setBulletSpeed(float newSpeed) { bulletSpeed = newSpeed; }
    public void setUnifBulletSpeed(Vector3 unifVec) { unifBulletVector = unifVec; }

    // Update is called once per frame
    void Update()
    {
        transform.position += unifBulletVector * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
