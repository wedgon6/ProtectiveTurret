using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class MovementPlayer : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _leftSparks;
    [SerializeField] private GameObject _rightSparks;

    private PlayerInput _playerInputSystem;
    private Vector3 _direction;
    private Rigidbody _rigidbody;
    private InputAction _move;
    private bool _isMoving = false;

    public float MoveSpeed => _moveSpeed;

    public void SetMoveSpeed(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        _maxMoveSpeed = _moveSpeed;
    }

    public void AddMoveSpeed()
    {
        _moveSpeed += 0.4f;
        _maxMoveSpeed += 0.4f;
    }

    public void SetModeMovmen(bool canMove)
    {
        if (canMove == true)
        {
            _playerInputSystem.Enable();
            _move = _playerInputSystem.Player.Move;
            _isMoving = true;
        }
        else
        {
            _playerInputSystem.Disable();
            _isMoving = false;
        }
    }

    private void Awake()
    {
        _playerInputSystem = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }

    private void FixedUpdate()
    {
        if (_isMoving == false)
            return;

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

        ShowFriction(horizontalVelocity.x);
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

    private void ShowFriction(float direction)
    {
        if (direction == 0)
        {
            _leftSparks.SetActive(false);
            _rightSparks.SetActive(false);
        }

        if(direction < 0)
        {
            _leftSparks.SetActive(false);
            _rightSparks.SetActive(true);
        }

        if(direction > 0)
        {
            _leftSparks.SetActive(true);
            _rightSparks.SetActive(false);
        }
    }
}
