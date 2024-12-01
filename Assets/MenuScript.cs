using System;
using SO.Echos;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private EchoEffectFactory echoEffectFactory;
    [SerializeField] private Echo echo;
    private PlayerInput _playerInput;
    private Camera _mainCamera;

    private void Start() {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Attack"].performed += OnMouseClick;
        _mainCamera = Camera.main;
    }

    public void OnMouseClick(InputAction.CallbackContext obj) {
        var cursorPosition = _playerInput.actions["Look"].ReadValue<Vector2>();
        // Shoot raycast from camera to cursor position
        var ray = _mainCamera.ScreenPointToRay(cursorPosition);
        if (Physics.Raycast(ray, out var hit)) {
            echoEffectFactory.CreateAndCastEchoEffect(hit.point, echo);
            if(hit.collider.CompareTag("PlayButton"))LoadNextScene();
        }
    }
    
    public void LoadNextScene() {
        _playerInput.actions["Attack"].performed -= OnMouseClick;
        SceneManager.LoadScene("Act_1");
    }
}
