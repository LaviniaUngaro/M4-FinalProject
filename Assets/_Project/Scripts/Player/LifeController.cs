using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Life")]
    [SerializeField] private int _currentHP = 100;
    [SerializeField] private int _maxHP = 100;

    private bool _isDead;

    [Header("Regeneration")]
    [SerializeField] private float _damageRegenTime = 4f; // tempo tra danno e rigenerazione
    [SerializeField] private int _regenRate = 2;
    [SerializeField] private float _regenPercent = 0.1f;

    private float _startRegenTime = 0f;
    private bool _needsRegen = false;
    private int _regenTarget;
    private float _regenBuffer = 0f;

    [SerializeField] private UnityEvent<int, int> _onLifeChanged;

    private GameManager _gameManager;
    private PlayerAnimations _playerAnimCon;
    private SoundManager _soundManager;

    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerAnimCon = GetComponentInChildren<PlayerAnimations>();
        _soundManager = FindAnyObjectByType<SoundManager>();
    }

    // gestione vita
    public int GetHP() => _currentHP;
    public int GetMaxHP() => _maxHP;
    public bool GetIsDead() => _isDead;

    public void SetHP(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHP);

        if (hp != _currentHP)
        {
            _currentHP = hp;
            _onLifeChanged.Invoke(_currentHP, _maxHP);
        }
    }

    public void TakeDamage(int amount)
    {
        if (_isDead) return;

        SetHP(_currentHP - amount);
        Debug.Log($"Hai subito {amount} di danno. Vita rimanente {GetHP()}");

        OnTakeDamage();
        _playerAnimCon.OnHit();
        _soundManager.OnDamage();

        if (GetHP() <= 0)
        {
            _isDead = true;
            _playerAnimCon.OnDeath(true);
            Die();

            return;
        }
    }

    public void Die()
    {
        _needsRegen = false;
        _gameManager.GameOver();
        _soundManager.OnDeath();
    }

    // gestione recupero vita
    private bool _regenCanStart => Time.time > _startRegenTime;

    private void RegenHP()
    {
        _regenBuffer += _regenRate * Time.deltaTime;
        int hpToAdd = Mathf.FloorToInt(_regenBuffer);

        if (hpToAdd > 0)
        {
            _regenBuffer -= hpToAdd;
            SetHP(_currentHP + hpToAdd);
        }

        Debug.Log($"Rigenerazione attiva. HP: {GetHP()}/{GetMaxHP()}");

        if (GetHP() >= _regenTarget)
        {
            SetHP(_regenTarget);
            _needsRegen = false;
            _regenBuffer = 0f;
        }
    }

    private void OnTakeDamage()
    {
        _needsRegen = true;
        _startRegenTime = Time.time + _damageRegenTime;

        _regenTarget = Mathf.Min(Mathf.RoundToInt(_currentHP * (1f + _regenPercent)), _maxHP);
    }

    void Update()
    {
        if (_needsRegen && _regenCanStart)
        {
            RegenHP();
        }
    }


    // FUNZIONI DI PROVA
    [ContextMenu("Danno")]
    public void Danno()
    {
        TakeDamage(10);
    }
}