using System;

public class HealthService : IHealthService
{
    private IHealthPresenter _healthPresenter;
    private ILogger _logger;
    private HealthConfig _healthConfig;

    private int _current;

    public event Action OnDied;

    public int Current => _current;
    public int Max => _healthConfig.MaxHealth;

    public float DamageMultiplier { get; set; } = 1f;

    public HealthService(HealthConfig healthConfig, ILogger logger, IHealthPresenter healthPresenter = null)
    {
        _healthConfig = healthConfig;
        _current = _healthConfig.MaxHealth;
        _healthPresenter = healthPresenter;
        _logger = logger;
    }

    public void TakeDamage(int amout)
    {
        if (amout <= 0)
        {
            return;
        }

        int finalAmount = (int)(amout * DamageMultiplier);

        _current -= finalAmount;
        _logger.Log("You took damage");

        if (_current <= 0)
        {
            _current = 0;
            _logger.Log("You died.");
            OnDied?.Invoke();
        }

        UpdatePresent();
    }

    public void Heal(int amout)
    {
        if (amout <= 0 || _current == _healthConfig.MaxHealth)
        {
            return;
        }

        _current += amout;
        _logger.Log("You healed");

        if (_current > _healthConfig.MaxHealth)
        {
            _current = _healthConfig.MaxHealth;
        }

        UpdatePresent();
    }

    private void UpdatePresent()
    {
        _healthPresenter?.OnHealthChanged(_current, _healthConfig.MaxHealth);
    }
}
