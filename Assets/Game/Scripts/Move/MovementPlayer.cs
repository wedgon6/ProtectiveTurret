using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private Camera _camera;

    private PlayerInput _playerInputSystem;
    private Vector3 _direction;
    private Rigidbody _rigidbody;
    private InputAction _move;

    private void Awake()
    {
        _playerInputSystem = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _playerInputSystem.Enable();
        _move = _playerInputSystem.Player.Move;
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _direction += _move.ReadValue<Vector2>().x * GetCameraRight(_camera) * _moveSpeed;
        _direction += _move.ReadValue<Vector2>().y * GetCameraForward(_camera) * _moveSpeed;

        _rigidbody.AddForce(_direction, ForceMode.Impulse);
        _direction = Vector3.zero;

        if (_rigidbody.velocity.y < 0f)
            _rigidbody.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = _rigidbody.velocity;
        horizontalVelocity.y = 0;

        if (horizontalVelocity.sqrMagnitude > _maxMoveSpeed * _maxMoveSpeed)
            _rigidbody.velocity = horizontalVelocity.normalized * _maxMoveSpeed + Vector3.up * _rigidbody.velocity.y;
    }

    private Vector3 GetCameraRight(Camera camera)
    {
        Vector3 forward = camera.transform.right;
        forward.y = 0;

        return forward.normalized;
    }

    private Vector3 GetCameraForward(Camera camera)
    {
        Vector3 right = camera.transform.forward;
        right.y = 0;

        return right.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Vector3.left*10);
    }
}
