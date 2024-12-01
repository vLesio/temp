using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ventilation : MonoBehaviour, IInteractable {
    [SerializeField] private AudioClip commentClip;
    [SerializeField] private PlayerInput input;
    [SerializeField] private bool isBlocking = true;
    [SerializeField] private AudioSource audioSource;
    private bool isPlaying = false;

    private void Start() {
        AudioSettings.Reset(AudioSettings.GetConfiguration());
        audioSource.UnPause();
        audioSource.Play();
    }

    public bool CanInteract() {
        return !isPlaying;
    }

    public void Interact() {
        StartCoroutine(PlayClip());
    }

    private IEnumerator PlayClip() {
        if(isBlocking) input.actions.Disable();
        AudioManager.I.PlayDialogue(commentClip);
        isPlaying = true;
        yield return new WaitForSecondsRealtime(commentClip.length);
        if(isBlocking) input.actions.Enable();
        isPlaying = false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
