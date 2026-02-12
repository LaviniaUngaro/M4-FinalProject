using TMPro;
using UnityEngine;

public class UI_Coins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;

    public void UpdateCoinsGraphics(int value)
    {
        _coinsText.text = string.Format("{0:00}", value);
    }
}