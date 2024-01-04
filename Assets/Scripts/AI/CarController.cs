using UnityEngine;
using UnityEngine.AI;

public class CarController : MonoBehaviour
{
    public Transform[] destinations;
    private NavMeshAgent agent;
    private int currentDestinationIndex = -1;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextDestination();
    }

    void MoveToNextDestination()
    {
        // Evita escolher o mesmo destino consecutivamente
        int nextDestinationIndex;
        do
        {
            nextDestinationIndex = Random.Range(0, destinations.Length);
        }
        while (destinations.Length > 1 && nextDestinationIndex == currentDestinationIndex);

        currentDestinationIndex = nextDestinationIndex;
        agent.SetDestination(destinations[currentDestinationIndex].position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToNextDestination();
        }
    }
}