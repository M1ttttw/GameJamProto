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
      
    // External Objects
    public GameObject attackRangeShape;
    public GameObject bullet;

    // Health
    public float health;

    // Send a bullet towards this target
    public abstract void attack(GameObject target);

    public void incrementTime() { attackCooldownTimer += Time.deltaTime; }
    public float getTime() { return attackCooldownTimer; }
    public void setTime(float value) { attackCooldownTimer = value; }
}
