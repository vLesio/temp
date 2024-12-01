using System.Collections;
using UnityEngine;

public class EmergencyBigDoor : MonoBehaviour, IInteractable {
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip accessDeniedClip;
    [SerializeField] private AudioClip doorOpenedComment;

    private bool isOpened = false;
    private bool isInteracting = false;
    private AudioSource _audioSource;
    private AudioManager _audioManager;
    private bool hasLoraResponded = false;
    private float _openAnimTime;

    public void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioManager = AudioManager.I;
        _openAnimTime = openSound.length;
    }
    
    public bool CanInteract() {
        if (isOpened || isInteracting) {
            return false;
        }
        return true;
    }

    public void Interact() {
        isInteracting = true;
        if (!GameState.HasDisabledGenerators) {
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
        isInteracting = false;
    }
    
    public IEnumerator OpenDoor() {
        while (_audioSource.isPlaying) {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        _audioSource.clip = openSound;
        _audioSource.Play();
        StartCoroutine(DoorOpenAnimation());
        isInteracting = false;
        isOpened = true;
    }
    
    IEnumerator DoorOpenAnimation() {
        var startPosition = transform.localPosition;
        var progress = 0f;
        var timePassed = 0f;
        while (progress < 1f) {
            timePassed += Time.deltaTime;
            progress = timePassed / _openAnimTime;
            transform.localPosition = Vector3.Lerp(startPosition, startPosition - new Vector3(0f, 2.25f, 0f), progress);
            yield return null;
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
