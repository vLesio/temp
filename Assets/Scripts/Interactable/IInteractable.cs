using UnityEngine;

public interface IInteractable {
    public bool CanInteract();
    public void Interact();
    public Transform GetTransform();
}
