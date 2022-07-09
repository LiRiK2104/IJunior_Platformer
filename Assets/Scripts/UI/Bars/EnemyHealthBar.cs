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

    protected override void Start()
    {
        base.Start();
        OnValueChanged(_enemy);
    }

    private void OnValueChanged(Enemy enemy)
    {
        CanvasGroup.alpha = enemy.Health < enemy.MaxHealth? MaxAlpha : MinAlpha;
        OnValueChanged(enemy.Health, enemy.MaxHealth);
    }
}
