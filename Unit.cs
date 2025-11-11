public class Unit
{
    protected string _name;
    protected int _maxHealth;
    protected int _currentHealth;
    protected int _damage;
    protected bool _isDead;

    public Unit (string name, int maxHealth, int damage)
    {
        _name = name;
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        _damage = damage;
        _isDead = false;
    }

    public string Name => _name;
    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;
    public int Damage => _damage;
    public bool IsDead => _isDead;

    public void TakeDamage(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            _isDead = true;
        }
    }

    public void Attack(Unit unit)
    {
        unit.TakeDamage(_damage);
    }
}
