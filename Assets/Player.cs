using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private Camera _mainCamera;
    void Start()
    {
        // Self components
        _mainCamera = Camera.main;
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponentInChildren<PlayerMovement>();
        // Children components
        
        RegisterPlayerInput();
    }

    private void Update() {
        CheckInputIfMove();
        UpdateOnLook();
    }

    private void RegisterPlayerInput() {
        _playerInput.actions["Jump"].performed += OnJump;
    }

    public void UpdateOnLook() {
        var mousePosition = _playerInput.actions["Look"].ReadValue<Vector2>();
        var mousePositionInScreen = new Vector3(mousePosition.x, mousePosition.y, 100);
        var worldPoint = _mainCamera.ScreenToWorldPoint(mousePositionInScreen);
        // log mouse and world point
        Debug.Log($"Mouse Position: {mousePosition}, World Point: {worldPoint}");
        _playerMovement.PlayerLookAtPosition(worldPoint);
    }

    public void OnJump(InputAction.CallbackContext obj) {
        throw new NotImplementedException();
    }

    private void CheckInputIfMove() {
        if (_playerInput.actions["Move"].IsPressed()) {
            _playerMovement.MovePlayerInDirection(_playerInput.actions["Move"].ReadValue<Vector2>());
        }
    }
}
