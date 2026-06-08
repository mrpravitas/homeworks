using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health;

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _health -= amount;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }
}
