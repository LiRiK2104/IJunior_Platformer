using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour, IHealthOwner
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _reward;
    
    private bool _isAlive;
    private int _health;
    private Animator _animator;
    private static readonly int DieState = Animator.StringToHash(EnemyAnimator.States.Die);
    private static readonly int DamageState = Animator.StringToHash(EnemyAnimator.States.TakeDamage);
    private static readonly int AttackState = Animator.StringToHash(EnemyAnimator.States.Attack);

    public static event Action<Enemy> Killing;
    public event Action<Enemy> TookDamage;
    
    public int MaxHealth => _maxHealth;
    public int Health => _health;
    public int Reward => _reward;
    
    public void Init()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
        
        gameObject.SetActive(true);
        _health = _maxHealth;
        _isAlive = true;
    }
    
    public void Attack(Player target)
    {
        _animator.SetTrigger(AttackState);
        _weapon.Shoot(target.transform.position);
    }

    public void TakeDamage(IDamagable damagableObject)
    {
        if (_isAlive == false)
            return;
        
        _animator.SetTrigger(DamageState);
        
        _health -= damagableObject.GetDamage();
        _health = Math.Max(_health, 0);

        TookDamage?.Invoke(this);

        if (_health == 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _isAlive = false;
        Killing?.Invoke(this);
        _animator.SetTrigger(DieState);

        float waitingTimeAfterDie = 1;
        yield return new WaitForSeconds(waitingTimeAfterDie);
        
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        _maxHealth = Math.Max(_maxHealth, 0);
        _reward = Math.Max(_reward, 0);
    }
}

public static class EnemyAnimator
{
    public static class States
    {
        public const string Idle = nameof(Idle);
        public const string Attack = nameof(Attack);
        public const string TakeDamage = nameof(TakeDamage);
        public const string Die = nameof(Die);
    }
}
