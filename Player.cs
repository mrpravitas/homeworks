public class Player : Unit
{
    private int _balance;
    private Weapon _equippedWeapon;
    private int _bonusDamage;

    public Player(string name, int maxHealth, int damage, int balance = 0) 
        : base(name, maxHealth, damage)
    {
        _balance = balance;
        _bonusDamage = 0;
    }

    public int Balance => _balance;
    public Weapon EquippedWeapon => _equippedWeapon;
    public int BonusDamage => _bonusDamage;
    public int Damage => _damage + _bonusDamage;

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

    public override void Attack(Unit unit)
    {
        unit.TakeDamage(_damage + _bonusDamage);
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

    public void EquipWeapon(Weapon weapon)
    {
        _equippedWeapon = weapon;
        _bonusDamage = weapon.DamageBonus;
    }

    public void UnequipWeapon()
    {
        _equippedWeapon = null;
        _bonusDamage = 0;
    }
}
