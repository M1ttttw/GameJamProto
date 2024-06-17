using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    protected String opponentTag = "Player";
    private GameObject[] cars;
    public float engageDistance=1.2f;
    public float strafeDistance=0.5f;
    private float mind;
    public GameObject closestCar;
    private Vector3 anchorPosition;
    public bool hasTarget = false;
    private IEnumerator strafe;
    void Start()
    {
        //on creation we find closest car and nav to it
        closestCar = FindClosestCar();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(closestCar.transform.position);
        strafe = Strafe();
        
    }
    public GameObject getCC(){
        return closestCar;
    }
    //finds closest car to enemy by just finding all gameobjects with player tag and finding the closest one
    private GameObject FindClosestCar(){
        mind = 100000f;
        cars = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject car in cars)
        {
            if (Vector3.Distance(this.transform.position,car.transform.position) < mind){
                mind = Vector3.Distance(this.transform.position,car.transform.position);
                closestCar = car;
            }
        }
        return closestCar;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }
    
    // Coroutine for strafing
    public IEnumerator Strafe()
    {
        Debug.Log("strafe");
        agent.speed = 1;
        while (true){
            //set dest to a random nearby location
            agent.SetDestination(new Vector3(this.transform.position.x+UnityEngine.Random.Range(-strafeDistance, strafeDistance),this.transform.position.y+UnityEngine.Random.Range(-strafeDistance, strafeDistance),this.transform.position.z));
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
            //return enemy to original position so that it doesnt straft too far away
            agent.SetDestination(anchorPosition);
            yield return new WaitForSeconds(1f);
        }
    }

    void wait()
    {
        Debug.Log("wait");
        anchorPosition = this.transform.position;
        //stops pathfinding
        agent.SetDestination(this.transform.position);
        StartCoroutine(strafe);
    }
    public void reTarget(){
        hasTarget = false;
        Debug.Log("retarget");
        agent.speed = 1f;
        StopCoroutine(strafe);
        closestCar=FindClosestCar();
        agent.SetDestination(closestCar.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(opponentTag))
        {
            //added just in case we strafes out of range (which shouldn't happen)
            if (hasTarget == false){
                //calls the wait function after x seconds so that the enemy has a chance to go further into the targeting range and strafing wont break aggro
                hasTarget = true;
                Invoke("wait", engageDistance);
                
            }
            
        }

    }
}


