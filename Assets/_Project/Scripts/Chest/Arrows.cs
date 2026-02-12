using UnityEngine;

public class Arrows : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _levelLayer;

    private LifeController _playerLife;
    private Rigidbody _rb;
    private Vector3 _direction;
    private int _damage;

    public float GetSpeed() => _speed;
    public int GetDamage() => _damage;

    public void SetSpeed(float value) => _speed = value;
    public void SetDamage(int value) => _damage = value;
    public void SetDirection(Vector3 direction) => _direction = direction;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _playerLife = collision.gameObject.GetComponent<LifeController>();

        if (collision.gameObject.layer == _levelLayer)
        {
            Destroy(gameObject);
        }

        if (_playerLife != null)
        {
            _playerLife.TakeDamage(_damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}