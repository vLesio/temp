using SO.Echos;
using UnityEngine;
using UnityEngine.AI;

public class CastEchoLoopIfMoving : MonoBehaviour {
    [SerializeField] private Echo echo;
    [SerializeField] private float castInterval = 0.5f;
    [SerializeField][Range(0,1)] private float randomBias = 0.1f;
    private EchoEffectFactory _echoEffectFactory;
    private NavMeshAgent _navMeshAgent;
    private float _timeSinceLastCast;
    private float _evaluatedCastInterval = 0f;
    
    private void Start() {
        _echoEffectFactory = GetComponentInChildren<EchoEffectFactory>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        EvaluateCastInterval();
    }
    
    private void Update() {
        _timeSinceLastCast += Time.deltaTime;
        if (_timeSinceLastCast > _evaluatedCastInterval && _navMeshAgent.velocity.magnitude > 0.1f) {
            _timeSinceLastCast = 0;
            EvaluateCastInterval();
            _echoEffectFactory.CreateAndCastEchoEffect(transform.position, echo);
        }
    }
    
    private void EvaluateCastInterval() {
        var maxDiff = castInterval * randomBias;
        _evaluatedCastInterval = castInterval + Random.Range(-maxDiff, 0);
    }
}
