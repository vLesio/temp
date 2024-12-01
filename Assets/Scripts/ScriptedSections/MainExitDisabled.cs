using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainExitDisabled : MonoBehaviour, IInteractable {
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private PlayerInput input;
    private bool isPlaying;

    public bool CanInteract() {
        return !isPlaying;
    }

    public void Interact() {
        StartCoroutine(Sequence());
    }
    
    private IEnumerator Sequence() {
        input.actions.Disable();
        foreach (var clip in clips) {
            AudioManager.I.PlayDialogue(clip);
            yield return new WaitForSecondsRealtime(clip.length + 0.5f);
        }
        input.actions.Enable();
    }

    public Transform GetTransform() {
        return transform;
    }
}
