using System;

public class EnemyHealth : Health
{
    public static event Action OnEnemyKilled;
    public event Action OnDied;

    protected override void Die()
    {
        OnEnemyKilled?.Invoke();
        OnDied?.Invoke();
    }

    public void Reset()
    {
        _currentHealth = MaxHealth;
    }
}
