using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 0.2f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private SoundManager _soundManager;

    private Rigidbody _rb;
    private Camera _mainCamera;
    private Vector3 _currentDirection;
    private GroundChecker _groundedCheck;

    private PlayerAnimations _playerAnimCon;
    private LifeController _playerLife;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _groundedCheck = GetComponentInChildren<GroundChecker>();
        _playerAnimCon = GetComponentInChildren<PlayerAnimations>();
        _playerLife = GetComponent<LifeController>();
    }

    void Update()
    {
        if (_playerLife.GetIsDead()) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        _currentDirection = _mainCamera.transform.forward * v + _mainCamera.transform.right * h;
        _currentDirection.y = 0f;

        float move = _currentDirection.magnitude;

        if (move > 0.01f && _groundedCheck.GetIsGrounded())
        {
            _currentDirection.Normalize();
            _soundManager.OnWalk();
        }

        _playerAnimCon.SetSpeed(move);

        if (Input.GetButtonDown("Jump") && _groundedCheck.GetIsGrounded())
        {
            _playerAnimCon.OnJump();
            _soundManager.OnJump();
            _rb.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (_playerLife.GetIsDead()) return;

        _rb.MovePosition(_rb.position + _currentDirection * _moveSpeed * Time.fixedDeltaTime);

        if (_currentDirection.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_currentDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed);
        }

        _playerAnimCon.OnIsGroundedChanged(_groundedCheck.GetIsGrounded());
        _playerAnimCon.SetVerticalSpeed();
    }
}