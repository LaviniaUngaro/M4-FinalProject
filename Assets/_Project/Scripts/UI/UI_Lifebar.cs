using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lifebar : MonoBehaviour
{
    [SerializeField] private Image _lifebar;
    [SerializeField] private TextMeshProUGUI _lifeText;

    public void UpdateLifeGraphics(int currentHP, int maxHP)
    {
        _lifeText.text = currentHP + "/" + maxHP;
        _lifebar.fillAmount = (float)currentHP / maxHP;
    }
}