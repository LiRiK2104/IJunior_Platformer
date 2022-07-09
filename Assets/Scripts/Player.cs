using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(
    typeof(PlayerMove), 
    typeof(PlayerMove))]
public class Player : MonoBehaviour, IHealthOwner
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Weapon _weapon;

    private static readonly int TakeDamageState = Animator.StringToHash(PlayerAnimator.States.TakeDamage);
    private PlayerMove _playerMove;
    private PlayerInput _playerInput;
    private int _health;
    
    
    public static event Action Killing;
    public static event Action<Player> TookDamage;

    public PlayerMove PlayerMove => _playerMove;
    public Weapon Weapon => _weapon;
    public int MaxHealth => _maxHealth;
    public int Health => _health;

    private void Awake()
    {
        Init();
    }
    
    public void TakeDamage(IDamager damagerObject)
    {
        _health -= damagerObject.Damage;
        _health = Math.Max(_health, 0);

        _playerInput.Animator.SetTrigger(TakeDamageState);
        TookDamage?.Invoke(this);
        
        if (_health == 0)
            Die();
    }

    public void SetWeapon(WeaponStoreCell cell)
    {
        Destroy(_weapon.gameObject);
        SetWeapon(cell.Data.Weaapon);
    }
    
    private void SetWeapon(Weapon template)
    {
        _weapon = Instantiate(template, transform.position, quaternion.identity, transform);
    }

    private void Init()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerInput = GetComponent<PlayerInput>();
        gameObject.SetActive(true);
        _health = _maxHealth;
        SetWeapon(_weapon);
    }

    private void Die()
    {
        Killing?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        _maxHealth = Math.Max(_maxHealth, 0);
    }
}

public interface IDamager
{
    public int Damage { get; }
}

public interface IHealthOwner
{
    public int Health { get; }

    public void TakeDamage(IDamager damagerObject);
}
