using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private int _fallingLimit = -20;

    private LifeController _lifePlayer;
    private bool _isGameOver = false;
    private bool _isWin = false;
    private SoundManager _soundManager;
    private BackgroundMusicManager _backgroundMusicManager;

    [SerializeField] private UnityEvent _onGameOver;
    [SerializeField] private UnityEvent _onWin;

    void Awake()
    {
        _lifePlayer = _player.GetComponent<LifeController>();
        _soundManager = FindAnyObjectByType<SoundManager>();
        _backgroundMusicManager = FindAnyObjectByType<BackgroundMusicManager>();
    }

    public bool GetIsGameOver() => _isGameOver;
    public bool GetIsWin() => _isWin;

    private void FallingPlayer()
    {
        if (_player != null)
        {
            if (_player.transform.position.y <= _fallingLimit)
            {
                _lifePlayer.Die();
            }
        }
    }

    public void GameOver()
    {
        if (_isGameOver) return;
        if (_camera != null)
        {
            _camera._lockRotation = true;
        }

        _isGameOver = true;

        Invoke(nameof(InvokeGameOver), 2);
    }

    public void InvokeGameOver()
    {
        _onGameOver.Invoke();
        _soundManager.OnGameOver();
        _backgroundMusicManager.StopBackgroundMusic();
    }

    public void Win()
    {
        if (_isWin) return;
        if (_camera != null)
        {
            _camera._lockRotation = true;
        }

        _isWin = true;

        _onWin.Invoke();
        _soundManager.OnWin();
        _backgroundMusicManager.StopBackgroundMusic();
    }


    void Update()
    {
        if (_isGameOver) return;
        FallingPlayer();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}