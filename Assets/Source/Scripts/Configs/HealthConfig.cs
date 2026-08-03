using UnityEngine;

[CreateAssetMenu(fileName = "HealthConfig", menuName = "Configs/Health Config")]
public class HealthConfig : ScriptableObject
{
    [SerializeField] private int _maxHealth;

    public int MaxHealth => _maxHealth;
}
