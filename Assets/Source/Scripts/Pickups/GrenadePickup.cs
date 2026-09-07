public class GrenadePickup : Pickup
{
    protected override ItemManager GetManager(GamePlayer player)
    {
        return player.GetComponent<GrenadeManager>();
    }
}