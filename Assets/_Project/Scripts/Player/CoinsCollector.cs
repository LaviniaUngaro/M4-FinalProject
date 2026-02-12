using UnityEngine;
using UnityEngine.Events;

public class CoinsCollector : MonoBehaviour
{
    [SerializeField] private int _coinsToWin;
    [SerializeField] private float _bonusTime = 30f;
    [SerializeField] private int _minChaosCoin = -3;
    [SerializeField] private int _maxChaosCoin = 8;

    private int _coinsCounter;
    private Timer _gameTimer;
    private GameManager _gameManager;
    private SoundManager _soundManager;

    [SerializeField] private UnityEvent<int> _coinsCollecting;

    void Awake()
    {
        _gameTimer = FindObjectOfType<Timer>();
        _gameManager = FindObjectOfType<GameManager>();
        _soundManager = FindAnyObjectByType<SoundManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Coins>(out Coins coin))
        {
            int value = CoinEffect(coin);
            _coinsCounter += value;
            _coinsCollecting.Invoke(_coinsCounter);
            if (coin.GetCoinType() == Coins.COINTYPE.Coin)
            {
                Debug.Log($"Hai trovato una Moneta che vale {coin.GetValue()}!");
            }
            else if (coin.GetCoinType() == Coins.COINTYPE.Chaos)
            {
                Debug.Log($"Hai trovato una Moneta Chaos! Il suo valore è di {value}. Ti ha fatto guadagnare o perdere monete?");
            }

            Destroy(other.gameObject);
            AllCoinsCollected();
        }
    }

    private int CoinEffect(Coins coin)
    {
        switch (coin.GetCoinType())
        {
            case Coins.COINTYPE.Coin:
                _soundManager.OnCoinsCollection();
                return coin.GetValue();
            case Coins.COINTYPE.Chaos:
                _soundManager.OnCoinsCollection();
                return Random.Range(_minChaosCoin, _maxChaosCoin);
            case Coins.COINTYPE.Time:
                _soundManager.OnTimeCoinCollection();
                _gameTimer.AddTime(_bonusTime);
                Debug.Log($"Hai trovato una Moneta Tempo! Ora hai +{_bonusTime} secondi");
                return 0;
            default:
                return coin.GetValue();
        }
    }

    private void AllCoinsCollected()
    {
        if (_coinsCounter >= _coinsToWin)
        {
            _gameManager.Win();
        }
    }
}