public abstract class Item
{
    protected string _name;
    protected int _cost;

    public abstract void Use(Player player);
}