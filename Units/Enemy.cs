public class Enemy : Unit
{
    private int _reward;

    public Enemy(string name, int maxHealth, int damage, int reward) 
        : base(name, maxHealth, damage)
    {
        _reward = reward;
    }

    public int Reward => _reward;

    public bool IsDefeated => _currentHealth <= 0;
}
