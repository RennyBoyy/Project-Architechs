using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public Transform[] destinations;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextDestination();
    }

    void MoveToNextDestination()
    {
        int randomIndex = Random.Range(0, destinations.Length);
        agent.SetDestination(destinations[randomIndex].position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToNextDestination();

        }
    }

}