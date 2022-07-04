using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New WeaponData", menuName = "Weapon Data/Create WeaponData", order = 51)]
public class WeaponStoreData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public int Price => _price;
    public Weapon Weaapon => _weapon;
    public Sprite Icon => _icon;

    private void OnValidate()
    {
        _price = Math.Max(_price, 0);
    }
}
