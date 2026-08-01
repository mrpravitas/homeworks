using UnityEngine.SceneManagement;

public class HealthService : IHealthService
{
    private int _max = 100;
    private int _current;

    private IHealthPresenter _healthPresenter;

    public int Current => _current;
    public int Max => _max;

    public HealthService(IHealthPresenter healthPresenter = null)
    {
        _current = _max;
        _healthPresenter = healthPresenter;
    }

    public void TakeDamage(int amout)
    {
        if (amout <= 0)
        {
            return;
        }

        _current -= amout;

        if (_current <= 0)
        {
            _current = 0;
            RestartScene();
        }

        UpdatePresent();
    }

    public void Heal(int amout)
    {
        if (amout <= 0)
        {
            return;
        }

        _current += amout;

        if (_current > _max)
        {
            _current = _max;
        }

        UpdatePresent();
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdatePresent()
    {
        _healthPresenter?.OnHealthChanged(_current, _max);
    }
}
