using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour, IInteractable {
    [SerializeField] private bool canBeOpened;
    [SerializeField] private bool isOpened;
    
    [Header("Audio")]
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closedSound;

    private AudioManager _audioManager;
    
    public void Start() {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
            isOpened = true;
        }
        else {
            _audioManager.PlayShot(closedSound);
        }
    }
}
