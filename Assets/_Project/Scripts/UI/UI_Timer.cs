using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private int _minutes;
    private int _seconds;

    public void UpdateTimerGraphics(float remainingTime)
    {
        _minutes = Mathf.FloorToInt(remainingTime / 60);
        _seconds = Mathf.FloorToInt(remainingTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }
}