public class Assassin : Enemy
{
    private Random _random = new Random();
    private float _dodgeChance;

    public Assassin(string name, int maxHealth, int damage, int reward, float dodgeChance) 
        : base(name, maxHealth, damage, reward)
    {
        _dodgeChance = dodgeChance;
    }

    public override void TakeDamage(int amount)
    {
        if (_random.NextDouble() < _dodgeChance)
        {
            return;
        }

        base.TakeDamage(amount);
    }
}
