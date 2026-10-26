using UnityEngine;

public enum PickupType
{
    Key,
    Chest
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType _type;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PlayerCollect player))
            return;

        if (player.TryCollect(_type))
            Destroy(gameObject);
    }
}
