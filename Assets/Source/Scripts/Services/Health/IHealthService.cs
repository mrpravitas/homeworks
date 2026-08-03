using System;

public interface IHealthService
{
    int Current {  get; }
    int Max { get; }

    event Action OnDied;

    void TakeDamage(int amout);
    void Heal(int amout);
}
