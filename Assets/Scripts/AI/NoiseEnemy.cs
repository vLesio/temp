using UnityEngine;
using UnityEngine.AI;

public class NoiseEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float walkDistance = 16f;

    private float speed = 0.75f;

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    public void PathFindToNoise(Vector3 noiseSource){
        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(noiseSource);
        if(agent.pathStatus != NavMeshPathStatus.PathComplete || agent.path == null){
            StopEnemy();
            return;
        }

        // checking detection distance
        if(Vector3.Distance(transform.position , noiseSource) > detectionDistance){
            StopEnemy();
            return;
        }

        // checking walk distance
        float distance = 0;
        for(int i = 0; i < agent.path.corners.Length - 2; i++){
            distance += Vector3.Distance(agent.path.corners[i] , agent.path.corners[i+1]);
        }
        if(distance > walkDistance){
            StopEnemy();
            return;
        }
    }

    private void StopEnemy(){
        agent.SetDestination(transform.position);
            agent.isStopped = true;
    }
}
