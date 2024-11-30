using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private EchoEffectFactory _echoEffectFactory;
    [SerializeField] private float yOffset = 3f;
    private void Start()
    {
        _echoEffectFactory = transform.parent.GetComponentInChildren<EchoEffectFactory>();
    }
    
    public void CastEchoEffect()
    {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position + Vector3.up * yOffset, 7, 250, Color.white);
    }

    public void CastFootStepEchoEffect() {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position + Vector3.up * yOffset, 2.5f, 250, Color.gray);
    }
}
