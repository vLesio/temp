using SO.Echos;
using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private EchoEffectFactory _echoEffectFactory;
    [SerializeField] private Echo footStepEcho;
    [SerializeField] private Echo snapEcho;
    private AudioSource _audioSource;
    private void Start()
    {
        _echoEffectFactory = transform.parent.GetComponentInChildren<EchoEffectFactory>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    
    public void CastSnapEchoEffect()
    {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, snapEcho);
        _audioSource.PlayOneShot(snapEcho.soundEffect);
    }

    public void CastFootStepEchoEffect() {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, footStepEcho);
        _audioSource.PlayOneShot(snapEcho.soundEffect);
    }
}
