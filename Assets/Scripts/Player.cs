using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerEcho _playerEcho;
    private Camera _mainCamera;
    private Animator _animator;
    void Start()
    {
        // Self components
        _mainCamera = Camera.main;
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerEcho = GetComponent<PlayerEcho>();
        _animator = GetComponentInChildren<Animator>();
        // Children components
        
        RegisterPlayerInput();
    }

    private void Update() {
        CheckInputIfMove();
        UpdateOnLook();
    }

    private void RegisterPlayerInput() {
        _playerInput.actions["Jump"].performed += OnJump;
        _playerInput.actions["CastEcho"].performed += OnEchoCast;
    }

    public void UpdateOnLook() {
        var mousePosition = _playerInput.actions["Look"].ReadValue<Vector2>();
        var mousePositionInScreen = new Vector3(mousePosition.x, mousePosition.y, 100);
        var worldPoint = _mainCamera.ScreenToWorldPoint(mousePositionInScreen);
        _playerMovement.PlayerLookAtPosition(worldPoint);
    }
    
    public void OnEchoCast(InputAction.CallbackContext obj) {
        Debug.Log("Echo Casted");
        _animator.SetTrigger("Snap");
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