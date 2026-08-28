using UnityEngine;

public class GrenadePickup : Pickup
{
    private void OnTriggerEnter(Collider other)
    {
        if (!_isAvailable)
        {
            return;
        }

        GamePlayer player = other.GetComponent<GamePlayer>();
        if (player == null)
        {
            return;
        }

        GrenadeManager grenadeManager = player.GetComponent<GrenadeManager>();
        if (grenadeManager == null)
        {
            return;
        }

        grenadeManager.TryPickup(this);
    }
}
