using UnityEngine;
using UnityEngine.UI;

public class WeaponStoreCell : MonoBehaviour
{
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Image _equippedState;
    [SerializeField] private Image _iconImage;

    private Player _player;
    private Store _store;
    private WeaponStoreData _data;
    private bool _isBought;
    private bool _isEquipped;

    public WeaponStoreData Data => _data;

    public void Init(Store store, Player player, WeaponStoreData data)
    {
        _player = player;
        _store = store;
        _data = data;
        _nameText.text = _data.Name;
        _priceText.text = _data.Price.ToString();
        _iconImage.sprite = _data.Icon;
        
        _isBought = false;
        _isEquipped = false;

        if (_data.Weaapon.Id == player.Weapon.Id)
        {
            _isBought = true;
            Equip();
        }
    }
    
    public void Unequip()
    {
        if (_isBought == false)
            return;
        
        _isEquipped = false;
        DisableAllStates();
        _equipButton.gameObject.SetActive(true);
    }
    
    private void OnEnable()
    {
        _buyButton.onClick.AddListener(Buy);
        _equipButton.onClick.AddListener(Equip);
        
        DisableAllStates();

        if (_isEquipped)
            _equippedState.gameObject.SetActive(true);
        else if (_isBought)
            _equipButton.gameObject.SetActive(true);
        else
            _buyButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(Buy);
        _equipButton.onClick.RemoveListener(Equip);
    }
    
    private void Buy()
    {
        if (_store.TryBuy(this))
        {
            _isBought = true;
            Equip();
        }
    }
    
    private void Equip()
    {
        if (_isBought == false)
            return;

        _isEquipped = true;
        
        DisableAllStates();
        _equippedState.gameObject.SetActive(true);
        
        _player.SetWeapon(this);
        _store.UnequipExcept(this);
    }

    private void DisableAllStates()
    {
        _buyButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);
        _equippedState.gameObject.SetActive(false);
    }
}
