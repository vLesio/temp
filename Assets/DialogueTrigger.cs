using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool isBlocking;
    [SerializeField] private PlayerInput input;
    private bool hasBeenTriggered;
    private void OnTriggerEnter(Collider other) {
        if (hasBeenTriggered || !other.CompareTag("Player")) return;
        hasBeenTriggered = true;
        StartCoroutine(Sequence());
    }
    
    private IEnumerator Sequence() {
        if(isBlocking) input.actions.Disable();
        foreach (var clip in clips) {
            AudioManager.I.PlayDialogue(clip);
            yield return new WaitForSecondsRealtime(clip.length + 0.5f);
        }
        if(isBlocking) input.actions.Enable();
    }
}
