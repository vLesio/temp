using System;
using System.Collections;
using UnityEngine;

public class GeneratorSwitch : MonoBehaviour, IInteractable {
    [SerializeField] private GameObject generatorOneAudioIndicators;
    [SerializeField] private GameObject generatorTwoAudioIndicators;
    [SerializeField] private AudioSource generatorOneAudio;
    [SerializeField] private AudioSource generatorTwoAudio;

    [Header("Audio")] [SerializeField] private AudioClip alarm;
    [SerializeField] private AudioClip aiVoiceLoop;
    [SerializeField] private AudioClip goBackToEmergencyClip;

    private AudioSource _globalAudio;
    private bool hasInteracted = false;

    private void Start() {
        _globalAudio = GetComponent<AudioSource>();
    }

    public bool CanInteract() {
        return !hasInteracted;
    }

    public void Interact() {
        GameState.HasDisabledGenerators = true;
        hasInteracted = true;
        generatorOneAudio.loop = false;
        generatorTwoAudio.loop = false;
        generatorOneAudio.Stop();
        generatorTwoAudio.Stop();
        generatorOneAudioIndicators.SetActive(false);
        generatorTwoAudioIndicators.SetActive(false);
        _globalAudio.loop = true;
        _globalAudio.clip = alarm;
        _globalAudio.Play();
        StartCoroutine(PlayAIInRepeat());
    }

    public Transform GetTransform() {
        return transform;
    }

    private IEnumerator PlayAIInRepeat() {
        AudioManager.I.PlayDialogue(goBackToEmergencyClip);
        yield return new WaitForSecondsRealtime(goBackToEmergencyClip.length + 1f);
        while (true) {
            AudioManager.I.PlayDialogue(aiVoiceLoop);
            yield return new WaitForSecondsRealtime(aiVoiceLoop.length + 7f);
        }
    }
}
