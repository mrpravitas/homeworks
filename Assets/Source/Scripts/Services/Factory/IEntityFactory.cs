using UnityEngine;

public interface IEntityFactory
{
    float SpawnChance { get; set; }

    void Create(Vector3 position);
}
