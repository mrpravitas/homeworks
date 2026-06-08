using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
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

    private void Die()
    {

    }
}
