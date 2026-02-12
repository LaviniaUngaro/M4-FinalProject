using UnityEngine;

public class ChestsControl : MonoBehaviour
{
    [SerializeField] private Arrows _arrowPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _shotPoint, _chestMonster;
    [SerializeField] private int _damage;
    [SerializeField] private float _fireRate = 0.8f;

    [Header("Distances")]
    [SerializeField] private float _aggroDistance = 10f;
    [SerializeField] private float _distance = 15f;
    [SerializeField] private float _speedRotation = 180f;
    private bool _playerNear = false;
    private bool _playerAggro = false;

    [Header("Animations")]
    [SerializeField] private string _paramPlayerNear = "playerNear";
    [SerializeField] private string _paramPlayerAggro = "playerAggro";

    private float _lastShotTime;
    private LifeController _playerLife;
    private Animator _anim;
    private SoundManager _soundManager;
    private bool _chestOpenSound = false;

    void Awake()
    {
        _playerLife = FindFirstObjectByType<LifeController>();
        _anim = GetComponent<Animator>();
        _soundManager = FindAnyObjectByType<SoundManager>();
    }

    void Update()
    {
        if (_player != null && !_playerLife.GetIsDead())
        {
            float d = Vector3.Distance(_player.position, transform.position);

            if (d <= _distance)
            {
                _playerNear = true;
            }
            else
            {
                _playerNear = false;
            }

            if (d <= _aggroDistance)
            {
                _playerAggro = true;
                
                if (!_chestOpenSound)
                {
                    _chestOpenSound = true;
                    _soundManager.OnChestOpening();
                }

                ChestRotation();
            }
            else
            {
                _playerAggro = false;
                _chestOpenSound = false;
            }

            _anim.SetBool(_paramPlayerNear, _playerNear);
            _anim.SetBool(_paramPlayerAggro, _playerAggro);

            if (_playerAggro && Time.time - _lastShotTime > _fireRate)
            {
                _lastShotTime = Time.time;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Vector3 direction = _player.position - _shotPoint.transform.position;
        direction.Normalize();

        Arrows arrow = Instantiate(_arrowPrefab, _shotPoint.position, Quaternion.LookRotation(direction));
        arrow.SetDamage(_damage);
        arrow.SetDirection(direction);
    }

    private void ChestRotation()
    {
        Vector3 direction = _player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _speedRotation * Time.deltaTime);
        }
    }
}