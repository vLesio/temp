using SO.Echos;
using UnityEngine;

public class PlayerEcho : MonoBehaviour
{
    private EchoEffectFactory _echoEffectFactory;
    [SerializeField] private Echo footStepEcho;
    [SerializeField] private Echo snapEcho;
    [SerializeField] private AudioClip snapEchoSound;
    private AudioManager audioMgr;

    private void Start()
    {
        HasAudioManager();
        _echoEffectFactory = transform.parent.GetComponentInChildren<EchoEffectFactory>();
    }
    
    private bool HasAudioManager(){
        if(audioMgr) return audioMgr;
        audioMgr = FindAnyObjectByType<AudioManager>();
        return audioMgr != null;
    }

    public void PlaySonarSound(){
        if(HasAudioManager()) audioMgr.PlayShot(snapEchoSound);
    }

    public void CastSnapEchoEffect()
    {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, snapEcho);
    }

    public void CastFootStepEchoEffect() {
        _echoEffectFactory.CreateAndCastEchoEffect(transform.position, footStepEcho);
    }
}
