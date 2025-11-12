public class Weapon : Item, IEquipable
{
    private int _damageBonus;

    public Weapon(string name, int cost, int damageBonus) 
    {
        _name = name;
        _cost = cost;
        _damageBonus = damageBonus;
    }

    public int DamageBonus => _damageBonus;
    public string Name => _name;
    public int Cost => _cost;

    public override void Use(Player player)
    {
        // TODO: flex
    }

    public void Equip(Player player)
    {
        player.EquipWeapon(this);
    }

    public void Unequip(Player player)
    {
        player.UnequipWeapon();
    }
}
