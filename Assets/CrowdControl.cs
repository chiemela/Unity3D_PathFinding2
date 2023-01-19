using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrowdControl : MonoBehaviour
{

    GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent agent;
    float detectionRadius = 10;
    float fleetRadius = 20;

    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        ResetAgent();
    }

    void ResetAgent()
    {
        agent.angularSpeed = 120;
        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newgoal = this.transform.position + fleeDirection * fleetRadius;
            agent.SetDestination(newgoal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1) 
        {
            ResetAgent();
            agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        }
    }
}
