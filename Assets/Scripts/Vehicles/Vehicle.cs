using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    // Attack Related Attributes
    public float attackStrength;
    protected float attackCooldownTimer;
    public float attackSpeed;
    public float bulletSpeed;

    // Aggro Management
    

    // External Objects
    public GameObject attackRangeShape;
    public GameObject bullet;

    // Health
    public float health;

    // Send a bullet towards this location
    public abstract void attack(Vector3 position);

    public void incrementTime() { attackCooldownTimer += Time.deltaTime; }
    public float getTime() { return attackCooldownTimer; }
    public void setTime(float value) { attackCooldownTimer = value; }
}
