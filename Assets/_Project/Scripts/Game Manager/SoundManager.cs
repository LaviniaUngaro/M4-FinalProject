using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _footstepSound;
    [SerializeField] private float _footstepInterval;
    [SerializeField] private AudioClip _coinsCollectionSound;
    [SerializeField] private AudioClip _timeCoinCollectionSound;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _chestSound;
    [SerializeField] private AudioClip _chandelierSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _gameOverSound;

    private float _footstepTimer;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnWalk()
    {
        _footstepTimer -= Time.deltaTime;

        if (_footstepTimer <= 0)
        {
            _audioSource.PlayOneShot(_footstepSound);
            _footstepTimer = _footstepInterval;
        }
    }

    public void OnDamage()
    {
        _audioSource.PlayOneShot(_damageSound);
    }

    public void OnDeath()
    {
        _audioSource.PlayOneShot(_deathSound);
    }

    public void OnJump()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }

    public void OnCoinsCollection()
    {
        _audioSource.PlayOneShot(_coinsCollectionSound);
    }

    public void OnTimeCoinCollection()
    {
        _audioSource.PlayOneShot(_timeCoinCollectionSound);
    }

    public void OnChestOpening()
    {
        _audioSource.PlayOneShot(_chestSound);
    }

    public void OnChandelierFalling()
    {
        _audioSource.PlayOneShot(_chandelierSound);
    }

    public void OnWin()
    {
        _audioSource.PlayOneShot(_winSound);
    }

    public void OnGameOver()
    {
        _audioSource.PlayOneShot(_gameOverSound);
    }
}
