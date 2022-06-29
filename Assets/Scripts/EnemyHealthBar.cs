using UnityEngine;

public class EnemyHealthBar : Bar
{
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _enemy.TookDamage += OnValueChanged;
        OnValueChanged(_enemy);
    }

    private void OnDisable()
    {
        _enemy.TookDamage -= OnValueChanged;
    }

    private void OnValueChanged(Enemy enemy)
    {
        Slider.enabled = enemy.Health < enemy.MaxHealth;
        OnValueChanged(enemy.Health, enemy.MaxHealth);
    }
}
