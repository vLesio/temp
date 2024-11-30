using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Utility.Interactable {
    public class Interactionen : MonoBehaviour {
        [SerializeField] private float interactionRange;
        private IInteractable _interactableObject;
        private PlayerInput _playerInput;

        private void Start() {
            _playerInput = GetComponent<PlayerInput>();
            _playerInput.actions["Interact"].performed += OnInteraction;
        }

        private void Update() {
            var interactables = FindInteractiveObjects();
            _interactableObject = FindClosestOrNull(interactables);
        }

        void OnInteraction(InputAction.CallbackContext obj) {
            Debug.Log("Object:" + _interactableObject);
            if (_interactableObject != null) {
                Debug.Log("Interaction");
                _interactableObject.Interact();
            }
        }

        private List<IInteractable> FindInteractiveObjects() {
            List<IInteractable> foundInterfaces = new List<IInteractable>();

            Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
            
            foreach (var collider in colliders)
            {
                IInteractable foundInterface = collider.GetComponent<IInteractable>();
                if (foundInterface != null && foundInterface.CanInteract())
                {
                    foundInterfaces.Add(foundInterface);
                }
            }
            return foundInterfaces;
        }

        private IInteractable FindClosestOrNull(List<IInteractable> interactables) {
            if (interactables == null || interactables.Count == 0)
            {
                return null; // No interactables to process
            }

            IInteractable closest = null;
            float closestDistance = float.MaxValue;

            foreach (var interactable in interactables)
            {
                if (interactable is MonoBehaviour monoBehaviour)
                {
                    float distance = Vector3.Distance(monoBehaviour.transform.position, transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closest = interactable;
                    }
                }
            }

            return closest;
        }
            
    }
}