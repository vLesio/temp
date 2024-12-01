using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody _rigidbody;
    [SerializeField] private GameObject playerBody;
    private Animator _animator;

    private void Start() {
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        _animator.SetFloat("Speed", _rigidbody.linearVelocity.magnitude/speed);
    }

    public void MovePlayerInDirection(Vector2 direction) {
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        playerBody.transform.LookAt(transform.position + moveDirection);
        var wishSpeed = speed * moveDirection.normalized;
        _rigidbody.linearVelocity = Vector3.Lerp(_rigidbody.linearVelocity, wishSpeed, 20 * Time.deltaTime);
    }
    
    public void Jump() {
        throw new NotImplementedException();
    }
    
    public void PlayerLookAtPosition(Vector3 position) {
        Vector3 positionToLookAt = new Vector3(position.x, transform.position.y, position.z);
        playerBody.transform.LookAt(positionToLookAt);
    }
}
