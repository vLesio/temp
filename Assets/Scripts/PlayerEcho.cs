using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private EchoEffectFactory _echoEffectFactory;
    [SerializeField] private float yOffset = 3f;
    private void Start()
    {
        _echoEffectFactory = GetComponentInChildren<EchoEffectFactory>();
    }
    
    public void CastEchoEffect()
    {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position + Vector3.up * yOffset);
    }
}
