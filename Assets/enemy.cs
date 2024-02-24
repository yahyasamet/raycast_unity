using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject targetObject;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetObject = GameObject.FindGameObjectWithTag("objB");

        if (targetObject == null)
        {
            Debug.Log("ObjectB not found in the scene!");
        }
    }

    void Update()
    {
        if (targetObject != null)
        {
            // Check if ObjectB is within sight
            RaycastHit hit;
            if (Physics.Raycast(transform.position, targetObject.transform.position - transform.position, out hit))
            {
                if (hit.collider.CompareTag("objB"))
                {
                    // Move towards ObjectB if it's within sight
                    agent.SetDestination(targetObject.transform.position);
                     Debug.Log("ObjectB  found in the scene!");
                }
                else
                {
                    // Move randomly if ObjectB is not within sight
                    if (!agent.hasPath)
                    {
                        Vector3 randomPoint = RandomNavmeshLocation(10f);
                        agent.SetDestination(randomPoint);
                    }
                }
            }
        }
    }

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, radius, NavMesh.AllAreas);
        return navHit.position;
    }
}
