public class Tank : Enemy
{
    private int _defence;
    public Tank(string name, int maxHealth, int damage, int reward, int defence) 
        : base(name, maxHealth, damage, reward)
    {
        _defence = defence;
    }

    public override void TakeDamage(int amount)
    {
        int reduced = amount - _defence;
        
        if (reduced <= 0)
        {
            reduced = 1;
        }

        base.TakeDamage(reduced);
    }
}
