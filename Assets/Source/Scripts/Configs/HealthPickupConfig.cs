using UnityEngine;

[CreateAssetMenu(fileName = "HealthPickupConfig", menuName = "Configs/Health Pickup Config")]
public class HealthPickupConfig : ScriptableObject
{
    [SerializeField] private int _healthAmount;

    public int HealthAmount => _healthAmount;
}
