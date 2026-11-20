using System;

public class HealthService : IHealthService, IDisposable
{
    private IHealthPresenter _healthPresenter;
    private ILogger _logger;
    private HealthConfig _healthConfig;

    private IDisposable _damageSubscription;
    private IDisposable _healSubscription;

    private int _current;

    public event Action OnDied;

    public int Current => _current;
    public int Max => _healthConfig.MaxHealth;

    public float DamageMultiplier { get; set; } = 1f;

    public HealthService(HealthConfig healthConfig, ILogger logger, IMessageBus bus,
        IHealthPresenter healthPresenter = null)
    {
        _healthConfig = healthConfig;
        _current = _healthConfig.MaxHealth;
        _healthPresenter = healthPresenter;
        _logger = logger;

        _damageSubscription = bus.Subscribe<PlayerDamagedSignal>(OnDamaged);
        _healSubscription = bus.Subscribe<HealthPickupCollectedSignal>(OnHealed);
    }

    public void Dispose()
    {
        _damageSubscription.Dispose();
        _healSubscription.Dispose();
    }

    public void TakeDamage(int amout)
    {
        if (amout <= 0)
        {
            return;
        }

        int finalAmount = (int)(amout * DamageMultiplier);

        _current -= finalAmount;

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

        if (_current > _healthConfig.MaxHealth)
        {
            _current = _healthConfig.MaxHealth;
        }

        UpdatePresent();
    }

    private void OnDamaged(PlayerDamagedSignal signal)
    {
        TakeDamage(signal.Amount);
    }

    private void OnHealed(HealthPickupCollectedSignal signal)
    {
        Heal(signal.Amount);
    }

    private void UpdatePresent()
    {
        _healthPresenter?.OnHealthChanged(_current, _healthConfig.MaxHealth);
    }
}
