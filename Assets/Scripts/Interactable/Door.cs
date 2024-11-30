using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] private bool canBeOpened;
    [SerializeField] private bool isOpened;
    [SerializeField] private Transform doorObjTransform;
    
    [Header("Audio")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closedSound;

    private AudioManager _audioManager;
    private float _openAnimTime;
    private BoxCollider _collider;
    
    public void Start() {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _openAnimTime = 1.4f;
        _collider = GetComponent<BoxCollider>();
    }

    public bool CanInteract() {
        if (isOpened) {
            return false;
        }
        
        return true;
    }
    
    public void Interact() {
        if (isOpened) {
            return;
        }

        if (canBeOpened) {
            _audioManager.PlayShot(openSound);
            StartCoroutine(DoorOpenAnimation());
            isOpened = true;
        }
        else {
            _audioManager.PlayShot(closedSound);
        }
    }

    IEnumerator DoorOpenAnimation() {
        var startPosition = doorObjTransform.localPosition;
        var progress = 0f;
        var timePassed = 0f;
        while (progress < 1f) {
            timePassed += Time.deltaTime;
            progress = timePassed / _openAnimTime;
            doorObjTransform.localPosition = Vector3.Lerp(startPosition, startPosition - new Vector3(0f, 0f, 1.375f), progress);
            yield return null;
        }
    }
}
