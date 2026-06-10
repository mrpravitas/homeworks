using System;

public class EnemyHealth : Health
{
    public static event Action OnEnemyKilled;

    protected override void Die()
    {
        OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }
}
