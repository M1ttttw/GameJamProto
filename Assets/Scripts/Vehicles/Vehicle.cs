using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    // Attack Related Attributes
    public float armorReduction = 0.5f;
    public float attackStrength;
    protected float attackCooldownTimer;
    public float attackSpeed;
    public float bulletSpeed;
    public float bulletLifetime;
    public bool AP;
    public float recoil;

    // External Objects
    public GameObject attackRangeShape;
    public GameObject bullet;

    // Health
    public float health;
    public float armor;

    // Audio
    public AudioClip shootClip;
    public AudioClip hitClip;
    public AudioClip deathClip;

    public AudioManager audioManager;

    // Send a bullet towards this target
    public abstract void attack(GameObject target);
    public abstract void onBulletImpact(float dmg, bool isAP);
    public void incrementTime() { attackCooldownTimer += Time.deltaTime; }
    public float getTime() { return attackCooldownTimer; }
    public void setTime(float value) { attackCooldownTimer = value; }
}
