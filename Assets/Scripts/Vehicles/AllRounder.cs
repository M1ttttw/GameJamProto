using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllRounder : Vehicle
{
    public GameEvent onCarDeath;
    private float randStrafe;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void onBulletImpact(float dmg, bool isAP){
        if (armor>0){
            if(isAP){
                armor -= dmg;
            }else{
                armor -= dmg*armorReduction;
            }
        }else{
            health -= dmg;
            if (health < 0){
                onCarDeath.TriggerEvent();
                Destroy(this.gameObject);
            }
        }
    }
    public override void attack(GameObject target)
    {
        Vector3 global_position = target.transform.position;

        // Create a copy of a bullet, and store it for later use.
        GameObject bullet_copy = Instantiate(bullet, transform.position, transform.rotation);

        // Edit the copy of the bullet so it fires at the desired vector and speed
        BulletBehavior bulletBehavior = bullet_copy.GetComponent<BulletBehavior>();
        randStrafe = UnityEngine.Random.Range(-randStrafe,randStrafe);
        bulletBehavior.setUnifBulletSpeed(Vector3.Normalize(global_position + new Vector3(randStrafe,randStrafe,0) - gameObject.transform.position));
        bulletBehavior.setBulletSpeed(bulletSpeed);
        bulletBehavior.setLifetime(bulletLifetime);
        bulletBehavior.attackStrength = attackStrength;
        bulletBehavior.isAP = AP;
        bulletBehavior.isFriendly = true;
        
        // Next we want to make the bullet's sprite rotate and look at the direction of our target.
        Vector3 local_position = transform.InverseTransformPoint(global_position);
        float initial_angle = Mathf.Atan2(local_position.y+randStrafe, local_position.x+randStrafe) * Mathf.Rad2Deg + 90;

        bullet_copy.transform.Rotate(0, 0, initial_angle);
    }
}
