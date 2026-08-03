using UnityEngine;

[CreateAssetMenu(fileName = "MovementConfig", menuName = "Configs/Movement Config")]
public class MovementConfig : ScriptableObject
{
    [SerializeField] private float _movementSpeed;

    public float MovementSpeed => _movementSpeed;
}
