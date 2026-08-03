using UnityEngine;

[CreateAssetMenu(fileName = "DangerZoneConfig", menuName = "Configs/Danger Zone Config")]
public class DangerZoneConfig : ScriptableObject
{
    [SerializeField] private int _damagePerTick;
    [SerializeField] private float _timePerTick;
    [SerializeField] private float _scaleMultiplier;

    public int DamagePerTick => _damagePerTick;
    public float TimePerTick => _timePerTick;
    public float ScaleMultiplier => _scaleMultiplier;
}
