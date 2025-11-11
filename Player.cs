public class Player : Unit
{
    private int _balance;

    public Player(string name, int maxHealth, int damage, int balance = 0) 
        : base(name, maxHealth, damage)
    {
        _balance = balance;
    }

    public int Balance => _balance;

    public void Heal(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }

    public void AddCoins(int amount)
    {
        _balance += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (amount < 0 || amount < _balance)
        {
            return false;
        }
        
        _balance -= amount;
        return true;
    }
}
