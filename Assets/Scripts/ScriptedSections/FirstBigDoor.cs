using System.Collections;
using UnityEngine;

public class FirstBigDoor : MonoBehaviour, IInteractable {
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip accessDeniedClip;
    [SerializeField] private AudioClip accessGrantedClip;
    [SerializeField] private AudioClip leiraAccessDeniedClip;

    private bool isOpened = false;
    private bool isInteracting = false;
    private AudioSource _audioSource;
    private AudioManager _audioManager;
    private bool hasLoraResponded = false;

    public void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioManager = AudioManager.I;
    }
    
    public bool CanInteract() {
        if (isOpened || isInteracting) {
            return false;
        }
        return true;
    }

    public void Interact() {
        isInteracting = true;
        if (!GameState.HasFirstKeyCard) {
            StartCoroutine(PlayDoorClosed());
        }
        else {
            StartCoroutine(OpenDoor());
        }
    }

    public IEnumerator PlayDoorClosed() {
        _audioSource.clip = accessDeniedClip;
        _audioSource.Play();
        while (_audioSource.isPlaying) {
            yield return new WaitForSecondsRealtime(0.1f);
        }

        if (!hasLoraResponded) {
            hasLoraResponded = true;
            _audioManager.PlayDialogue(leiraAccessDeniedClip);
            yield return new WaitForSecondsRealtime(3f);
        }
        isInteracting = false;
    }
    
    public IEnumerator OpenDoor() {
        _audioSource.clip = accessGrantedClip;
        _audioSource.Play();
        while (_audioSource.isPlaying) {
            yield return new WaitForSecondsRealtime(0.1f);
        }

        _audioSource.clip = openSound;
        _audioSource.Play();
        // #TODO: Play animation
        isInteracting = false;
        isOpened = true;
    }
}
