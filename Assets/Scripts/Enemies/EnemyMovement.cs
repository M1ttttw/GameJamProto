using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    protected String opponentTag = "Player";
    public GameObject[] cars;
    private float mind;
    private GameObject closestCar;
    private Vector3 anchorPosition;
    private bool retarget = false;
    void Start()
    {
        //on creation we find closest car and nav to it
        closestCar = FindClosestCar();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(closestCar.transform.position);
        
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
    IEnumerator Strafe()
    {
        agent.speed = 1;
        while (true){
            //set dest to a random nearby location
            agent.SetDestination(new Vector3(this.transform.position.x+UnityEngine.Random.Range(-0.7f, 0.7f),this.transform.position.y+UnityEngine.Random.Range(-0.7f, 0.7f),this.transform.position.z));
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
            //return enemy to original position so that it doesnt straft too far away
            agent.SetDestination(anchorPosition);
            yield return new WaitForSeconds(1f);
        }
    }

    void wait()
    {
        //added just in case the enemy strafes out of range (which shouldn't happen)
        //if we didnt retarget an enemy we dont change the anchor position
        if (retarget == false){
            anchorPosition = this.transform.position;
        }
        //stops pathfinding
        agent.SetDestination(this.transform.position);
        StartCoroutine(Strafe());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(opponentTag))
        {
            //calls the wait function after 0.15 seconds so that the enemy has a chance to go further into the targeting range and strafing wont break aggro
            Invoke("wait", 0.15f);
            
        }

    }
}


