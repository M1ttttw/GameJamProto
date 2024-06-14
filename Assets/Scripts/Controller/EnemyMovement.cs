using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation=false;
        agent.updateUpAxis=false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        OnEnable();
    }

    private void OnEnable()
    {
        
        if (agent == null) {
            Start();
        }
        agent.SetDestination(target.position);
    }
}
