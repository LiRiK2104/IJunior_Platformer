using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    private int _money;

    public event Action<int> ChangedMoney; 

    public int Money => _money;
    
    public bool TryPay(WeaponStoreCell cell)
    {
        if (_money >= cell.Data.Price)
        {
            _money -= cell.Data.Price;
            ChangedMoney?.Invoke(_money);
            return true;
        }

        return false;
    }

    private void OnEnable()
    {
        Enemy.Killing += AddMoney;
    }

    private void OnDisable()
    {
        Enemy.Killing -= AddMoney;
    }

    private void AddMoney(Enemy enemy)
    {
        _money += enemy.Reward;
        ChangedMoney?.Invoke(_money);
    }
}