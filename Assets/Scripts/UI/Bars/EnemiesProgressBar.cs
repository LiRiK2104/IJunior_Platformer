
public class EnemiesProgressBar : Bar
{
    protected override void Awake()
    {
        base.Awake();
        ProtectedValue = 0;
    }

    private void OnEnable()
    {
        EnemySpawner.EnemyCountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        EnemySpawner.EnemyCountChanged -= OnValueChanged;
    }
}
