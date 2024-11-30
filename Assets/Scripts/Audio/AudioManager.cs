using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource dialogueSource;
    [SerializeField] private AudioSource defaultSoundSource;

    [Header("Sounds configuration")] [SerializeField]
    private GameObject soundObjectPrefab;
    private AudioSource[] soundSources;

    public static AudioManager I;
    
    void Awake() {
        if (I == null) {
            I = this;  
        }
    }

    public void PlayShot(AudioClip audioClip) {
        defaultSoundSource.PlayOneShot(audioClip);
    }

    public void PlayMusic(AudioClip musicClip) {
        if (!musicSource.isPlaying) {
            musicSource.clip = musicClip;
            musicSource.Play();
            return;
        }

        StartCoroutine(FadeOutMusicAndPlay(musicClip));
    }

    public void PlayDialogue(AudioClip audioClip) {
        dialogueSource.PlayOneShot(audioClip);
    }

    void StopMusic() {
        StartCoroutine(FadeOutMusic());
    }
    
    public IEnumerator FadeOutMusic(float fadeTime = 1f) {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0) {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
    }
    
    public IEnumerator FadeOutMusicAndPlay(AudioClip musicClip, float fadeTime = 1f) {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0) {
            musicSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume;
        musicSource.clip = musicClip;
        musicSource.Play();
    }
}
