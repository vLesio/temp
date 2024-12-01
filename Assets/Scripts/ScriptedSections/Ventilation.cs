using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ventilation : MonoBehaviour, IInteractable {
    [SerializeField] private AudioClip commentClip;
    [SerializeField] private PlayerInput input;
    [SerializeField] private bool isBlocking = true;
        
    private bool isPlaying = false;
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
