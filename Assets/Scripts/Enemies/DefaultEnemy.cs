using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DefaultEnemy : Vehicle
{
    private MainManager scoreKeeper;
    public GameEvent onEnemyDeath;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = GameObject.Find("Manager").GetComponent<MainManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void onBulletImpact(float dmg, bool isAP){
        audioManager.playClip(hitClip);
        if (armor>0){
            if(isAP){
                armor -= dmg;
            }else{
                armor -= dmg*armorReduction;
            }
        }else{
            health -= dmg;
            if (health <= 0){
                audioManager.playClip(deathClip);
                scoreKeeper.enemyDeathScore += score;
                Destroy(this.gameObject);
                onEnemyDeath.TriggerEvent();
            }
        }
    }

    public override void attack(GameObject target) {
        audioManager.playClip(shootClip);
        Vector3 global_position = target.transform.position;

        // Create a copy of a bullet, and store it for later use.
        GameObject bullet_copy = Instantiate(bullet, transform.position, transform.rotation);

        // Edit the copy of the bullet so it fires at the desired vector and speed
        BulletBehavior bulletBehavior = bullet_copy.GetComponent<BulletBehavior>();

        bulletBehavior.setUnifBulletSpeed(Vector3.Normalize(global_position - gameObject.transform.position));
        bulletBehavior.setBulletSpeed(bulletSpeed);
        bulletBehavior.setLifetime(bulletLifetime);
        bulletBehavior.attackStrength = attackStrength;
        bulletBehavior.isAP = AP;
        bulletBehavior.isFriendly = false;

        // Next we want to make the bullet's sprite rotate and look at the direction of our target.
        Vector3 local_position = transform.InverseTransformPoint(global_position);
        float initial_angle = Mathf.Atan2(local_position.y, local_position.x) * Mathf.Rad2Deg + 90;

        bullet_copy.transform.Rotate(0, 0, initial_angle);
    }
}
