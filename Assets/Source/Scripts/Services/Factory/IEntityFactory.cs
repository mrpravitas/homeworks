using UnityEngine;

public interface IEntityFactory<T> where T : Object
{
    void Create(Vector3 position);
}
