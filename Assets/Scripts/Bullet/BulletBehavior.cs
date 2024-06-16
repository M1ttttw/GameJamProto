using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float bulletSpeed;

    // Note that this vector must be of uniform length!
    private Vector3 unifBulletVector;
    private float lifetimeTimer = 0;
    private float lifetime;
    public bool isAP;
    public bool isFriendly;
    private Vehicle attackedVehicle;
    public float attackStrength;

    public void setBulletSpeed(float newSpeed) { bulletSpeed = newSpeed; }
    public void setUnifBulletSpeed(Vector3 unifVec) { unifBulletVector = unifVec; }
    public void setLifetime(float seconds) { lifetime = seconds; }

    // Update is called once per frame
    void Update()
    {
        transform.position += unifBulletVector * bulletSpeed * Time.deltaTime;

        if (lifetimeTimer < lifetime)
        {
            lifetimeTimer += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFriendly){
            if (collision.gameObject.tag == "Enemy"){
                attackedVehicle = collision.gameObject.GetComponent<Vehicle>();
                attackedVehicle.onBulletImpact(attackStrength,isAP);
                Destroy(this.gameObject);
            }
        }else{
            if (collision.gameObject.tag == "Player"){
                attackedVehicle = collision.gameObject.GetComponent<Vehicle>();
                attackedVehicle.onBulletImpact(attackStrength,isAP);
                Destroy(this.gameObject);
            }
        }
    }

}
