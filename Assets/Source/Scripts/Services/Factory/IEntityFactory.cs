using UnityEngine;

public interface IEntityFactory<T> where T : Object
{
    public float SpawnChance { get; set; }

    void Create(Vector3 position);
}
