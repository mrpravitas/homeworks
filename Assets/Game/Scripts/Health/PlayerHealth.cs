using System;

public class PlayerHealth : Health
{
    public static event Action<int> OnHealthChanged;
    public static event Action OnPlayerDeath;

    private void Start()
    {
        OnHealthChanged?.Invoke(base.MaxHealth);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        OnHealthChanged?.Invoke(base.CurrentHealth);
    }

    protected override void Die()
    {
        OnPlayerDeath?.Invoke();
    }
}
