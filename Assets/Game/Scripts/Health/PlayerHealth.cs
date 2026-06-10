using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : Health
{
    public static event Action<int> OnHealthChanged;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
