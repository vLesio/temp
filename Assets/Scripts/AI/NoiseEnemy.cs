using UnityEngine;
using UnityEngine.AI;

public class NoiseEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private float speed = 0.75f;

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    public void PathFindToNoise(Vector3 noiseSource){
        Debug.Log("Sound detected!");
        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(noiseSource);
        if(agent.pathStatus != NavMeshPathStatus.PathComplete || agent.path == null) return;
        float distance = 0;
        for(int i = 0; i < agent.path.corners.Length - 2; i++){
            distance += Vector3.Distance(agent.path.corners[i] , agent.path.corners[i+1]);
        }

        if(distance> 16f){
            agent.SetDestination(transform.position);
            Debug.Log("Too far...");
            agent.isStopped = true;
        }
    }
}
