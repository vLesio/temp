using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FryingPan : MonoBehaviour, IInteractable {
    [SerializeField] private AudioClip[] clips;
    private bool isPlaying;
    private int count = 0;

    public bool CanInteract() {
        return !isPlaying;
    }

    public void Interact() {
        StartCoroutine(PlayClip());
    }
    
    private IEnumerator PlayClip() {
        AudioManager.I.PlayDialogue(clips[count]);
        isPlaying = true;
        yield return new WaitForSecondsRealtime(clips[count].length);
        count = Mathf.Min(++count, clips.Length - 1);
        isPlaying = false;
    }

    public Transform GetTransform() {
        return transform;
    }
}
