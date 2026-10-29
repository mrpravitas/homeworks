using UnityEngine;

public enum PickupType
{
    Key,
    Chest
}

public class Pickup : MonoBehaviour
{
    [SerializeField] public PickupType _type;
}
