using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class NoiseEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float walkDistance = 16f;
    private Animator _animator;

    private Vector3 startPosition;
    private Coroutine backCor;
    private Vector3 lastChecked;

    [SerializeField] private float speed = 0.75f;

    private void Start()
    {
        startPosition = transform.position;
        lastChecked = startPosition;
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }

    private void Update() {
        _animator.SetFloat("Speed", Mathf.Min(agent.velocity.magnitude, 1));
    }

    public void PathFindToNoise(Vector3 noiseSource){
        
        // tymczasowo
        noiseSource.y = 0;

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

        if(backCor == null){
            lastChecked = noiseSource;
            backCor = StartCoroutine(GoBackWhenDone());
        }
    }

    public void PathFindToStart(){
        PathFindToPosition(startPosition);
    }

    public bool PathFindToPosition(Vector3 position){
        agent.speed = speed;
        agent.isStopped = false;
        agent.SetDestination(position);
        if(agent.pathStatus != NavMeshPathStatus.PathComplete || agent.path == null){
            StopEnemy();
            return false;
        }
        return true;
    }

    private void StopEnemy(){
        agent.SetDestination(transform.position);
            agent.isStopped = true;
    }

    private IEnumerator GoBackWhenDone(){
        float backTime = Random.Range(3,5);
        while(true){
            yield return null;
            if(Vector3.Distance(lastChecked , transform.position) < 0.1f) backTime -= Time.deltaTime;
            else backTime = 3;
            if(backTime < 0f){
                lastChecked = startPosition;
                PathFindToStart();
                backCor = null;
                yield break;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.GetComponent<Player>().PlayerDies();
        }
    }
}
