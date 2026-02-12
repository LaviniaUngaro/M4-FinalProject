using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _sensitivity;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _topDistance;
    [SerializeField] private float _bottomDistance;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private LayerMask _levelLayer;
    [SerializeField] private float _cameraRadius = 0.3f;

    private float _yaw;
    private float _pitch;

    public bool _lockRotation = false;

    void Update()
    {
        if (!_gameOverUI.activeInHierarchy && !_winUI.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        if (_lockRotation) return;

        _yaw += Input.GetAxis("Mouse X") * _sensitivity;
        _pitch -= Input.GetAxis("Mouse Y") * _sensitivity;
        _pitch = Mathf.Clamp(_pitch, _bottomDistance, _topDistance);
        Quaternion cameraRotation = Quaternion.Euler(_pitch, _yaw, 0);

        if (_target != null)
        {
            Vector3 cameraPosition = _target.position + cameraRotation * _offset;
            Vector3 direction = cameraPosition - _target.position;

            if (Physics.SphereCast(_target.position, _cameraRadius, direction.normalized, out RaycastHit hit, direction.magnitude, _levelLayer))
            {
                cameraPosition = hit.point - direction.normalized * 0.2f;
            }

            Quaternion lookRotation = Quaternion.LookRotation(_target.position - cameraPosition);
            transform.SetPositionAndRotation(cameraPosition, lookRotation);
        }
    }
}