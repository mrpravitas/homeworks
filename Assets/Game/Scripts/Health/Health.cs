using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    protected int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
