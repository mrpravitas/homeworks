public interface IHealthService
{
    int Current {  get; }
    int Max { get; }

    void TakeDamage(int amout);
    void Heal(int amout);
}
