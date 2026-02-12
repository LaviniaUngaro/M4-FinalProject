using UnityEngine;

public class Chandeliers : MonoBehaviour
{
    [SerializeField] private LifeController _playerLife;
    [SerializeField] private int _damage;

    private Rigidbody _rb;
    private bool _hasHitPlayer;
    private SoundManager _soundManager;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _soundManager = FindAnyObjectByType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasHitPlayer) return;

        if (other.GetComponent<LifeController>())
        {
            transform.SetParent(null);
            _rb.useGravity = true;
            _rb.isKinematic = false;
            _soundManager.OnChandelierFalling();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHitPlayer) return;

        if (collision.gameObject.GetComponent<LifeController>() && _rb.velocity.y < -1f)
        {
            _hasHitPlayer = true;
            _playerLife.TakeDamage(_damage);
        }
    }
}