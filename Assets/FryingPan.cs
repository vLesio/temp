using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FryingPan : MonoBehaviour, IInteractable {
    [SerializeField] private AudioClip clip;
    private bool isPlaying;

    public bool CanInteract() {
        return !isPlaying;
    }

    public void Interact() {
        StartCoroutine(PlayClip());
    }
    
    private IEnumerator PlayClip() {
        AudioManager.I.PlayDialogue(clip);
        isPlaying = true;
        yield return new WaitForSecondsRealtime(clip.length);
        isPlaying = false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
