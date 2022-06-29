
public class EnemiesProgressBar : Bar
{
    private void OnEnable()
    {
        EnemySpawner.EnemyCountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        EnemySpawner.EnemyCountChanged -= OnValueChanged;
    }
}
