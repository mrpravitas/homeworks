using UnityEngine;

public class EnemyConfig : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}
