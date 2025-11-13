public class Potion : Item
{
    private int _heal;
    private int _damageBoost;
    private int _duration;

    public Potion(string name, int heal, int damageBoost, int duration, int cost)
    {
        _name = name;
        _heal = heal;
        _damageBoost = damageBoost;
        _duration = duration;
        _cost = cost;
    }

    public string Name => _name;
    public int Heal => _heal;
    public int DamageBoost => _damageBoost;
    public int Duration => _duration;
    public int Cost => _cost;

    public override void Use(Player player)
    {
        player.Heal(_heal);
        player.IncreaseDamage(_damageBoost);
    }
}
