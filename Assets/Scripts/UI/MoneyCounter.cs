using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyCounter : MonoBehaviour
{
    private Text _text;
    private PlayerCurrency _playerCurrency;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _playerCurrency = FindObjectOfType<PlayerCurrency>();
    }

    private void OnEnable()
    {
        _playerCurrency.ChangedMoney += MoneyChanged;
        MoneyChanged(_playerCurrency.Money);
    }

    private void OnDisable()
    {
        _playerCurrency.ChangedMoney -= MoneyChanged;
    }

    private void MoneyChanged(int money)
    {
        _text.text = money.ToString();
    }
}