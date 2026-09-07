using UnityEngine;

public class MedKitManager : ItemManager
{
    [SerializeField] private Health _health;
    [SerializeField] private int _healAmount;

    protected override bool CanUse()
    {
        return !_health.IsFull;
    }

    protected override void Use()
    {
        _health.Heal(_healAmount);
    }
}