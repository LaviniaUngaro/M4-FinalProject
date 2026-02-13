using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _remainingTime;

    [SerializeField] private UnityEvent<float> _onTimePassing;
    [SerializeField] private UnityEvent _onTimesUp;
    [SerializeField] private GameManager _gameManager;

    public float GetRemainingTime() => _remainingTime;

    public void AddTime(float seconds)
    {
        _remainingTime += seconds;
    }

    private void Update()
    {
        if (_gameManager.GetIsGameOver()) return;
        if (_gameManager.GetIsWin()) return;

        if (_remainingTime >= 1)
        {
            _remainingTime -= Time.deltaTime;
            _onTimePassing.Invoke(_remainingTime);
        }
        else
        {
            _remainingTime = 0;
            _onTimesUp.Invoke();
            _gameManager.GameOver();
        }
    }
}
