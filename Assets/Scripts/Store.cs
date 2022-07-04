using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Store : Window
{
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private WeaponStoreCell _cellTemplate;
    [SerializeField] private List<WeaponStoreData> _weaponStoreDatas = new List<WeaponStoreData>();
    
    public override WindowType WindowType { get; } = WindowType.Store;
    private List<WeaponStoreCell> _cells = new List<WeaponStoreCell>();
    
    private Player _player;
    private PlayerCurrency _playerCurrency;

    public bool TryBuy(WeaponStoreCell cell)
    {
        if (_playerCurrency.TryPay(cell))
        {
            return true;
        }
        else
        {
            WindowFabric.Instance.Open((int)WindowType.FailWindow);
            //TODO: open failed purchase window
            return false;
        }
    }
    
    public void UnequipExcept(WeaponStoreCell cellForBuy)
    {
        foreach (var cell in _cells)
        {
            if (cell != cellForBuy)
                cell.Unequip();
        }
    }

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0;
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1;
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _playerCurrency = FindObjectOfType<PlayerCurrency>();
        Initialize();
    }

    private void Initialize()
    {
        foreach (var cell in _cells)
        {
            Destroy(cell);
        }
        
        _cells.Clear();
        _weaponStoreDatas.OrderBy(weapon => weapon.Price);
        
        foreach (var weaponStoreData in _weaponStoreDatas)
        {
            var weaponCell = Instantiate(_cellTemplate, _grid.transform.position, quaternion.identity, _grid.transform);
            weaponCell.Init(this, _player, weaponStoreData);
            _cells.Add(weaponCell);
        }
    }
}
