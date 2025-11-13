public class Weapon : Item, IEquipable
{
    private int _damageBonus;
    private bool _isPurchased;

    public Weapon(string name, int cost, int damageBonus) 
    {
        _name = name;
        _cost = cost;
        _damageBonus = damageBonus;
    }

    public int DamageBonus => _damageBonus;
    public string Name => _name;
    public int Cost => _cost;
    public bool IsPurchased => _isPurchased;

    public override void Use(Player player)
    {
        Console.WriteLine($"{player.Name} flexes with his new weapon, {this.Name}. Cool, right?");
        Console.ReadLine();
    }

    public void Equip(Player player)
    {
        player.EquipWeapon(this);
    }

    public void Unequip(Player player)
    {
        player.UnequipWeapon();
    }

    public bool Buy(Player player)
    {
        return _isPurchased = player.SpendCoins(_cost);
    }
}
