public class MedKitPickup : Pickup
{
    protected override ItemManager GetManager(GamePlayer player)
    {
        return player.GetComponent<MedKitManager>();
    }
}