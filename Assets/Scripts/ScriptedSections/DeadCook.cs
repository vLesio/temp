using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeadCook : MonoBehaviour, IInteractable {
    [SerializeField] private PlayerInput input;
    [SerializeField] private AudioClip[] clips;
    
    private bool hasInteracted = false;
    public bool CanInteract() {
        return !hasInteracted;
    }

    public void Interact() {
        hasInteracted = true;
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence() {
        input.actions.Disable();
        foreach (var clip in clips) {
            AudioManager.I.PlayDialogue(clip);
            yield return new WaitForSecondsRealtime(clip.length + 0.5f);
        }
        input.actions.Enable();
        GameState.HasFirstKeyCard = true;
    }

    public Transform GetTransform() {
        return transform;
    }
}
