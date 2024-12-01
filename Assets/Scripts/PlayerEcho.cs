using SO.Echos;
using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private EchoEffectFactory _echoEffectFactory;
    [SerializeField] private Echo footStepEcho;
    [SerializeField] private Echo snapEcho;
    private AudioManager audioMgr;

    private AudioSource _audioSource;
    private void Start()
    {
        HasAudioManager();
        _echoEffectFactory = transform.parent.GetComponentInChildren<EchoEffectFactory>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    
    private bool HasAudioManager(){
        if(audioMgr) return audioMgr;
        audioMgr = FindAnyObjectByType<AudioManager>();
        return audioMgr != null;
    }

    public void PlaySonarSound(){
        if(HasAudioManager()) audioMgr.PlayShot(snapEcho.soundEffect);
    }

    public void CastSnapEchoEffect()
    {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, snapEcho);
        // audio played through animation events
    }

    public void CastFootStepEchoEffect() {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, footStepEcho);
        _audioSource.PlayOneShot(footStepEcho.soundEffect);
    }
}
