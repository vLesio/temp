using SO.Echos;
using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private EchoEffectFactory _echoEffectFactory;
    [SerializeField] private Echo footStepEcho;
    [SerializeField] private Echo snapEcho;
    private void Start()
    {
        _echoEffectFactory = transform.parent.GetComponentInChildren<EchoEffectFactory>();
    }
    
    public void CastSnapEchoEffect()
    {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, snapEcho);
    }

    public void CastFootStepEchoEffect() {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, footStepEcho);
    }
}
