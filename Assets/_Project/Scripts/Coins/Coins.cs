using UnityEngine;

public class Coins : MonoBehaviour
{
    public enum COINTYPE { Coin, Chaos, Time }

    [SerializeField] private int _value;
    [SerializeField] private COINTYPE _coinType;

    public int GetValue() => _value;

    public COINTYPE GetCoinType() => _coinType;
}