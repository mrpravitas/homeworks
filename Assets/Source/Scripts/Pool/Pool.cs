using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Component, IPoolable
{
    private Stack<T> _items = new Stack<T>();
    private T _prefab;
    private Transform _parent;

    public Pool(T prefab, Transform parent, int prewarmCount)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < prewarmCount; i++)
        {
            T item = Object.Instantiate(_prefab, _parent);
            item.gameObject.SetActive(false);
            _items.Push(item);
        }
    }

    public T Get()
    {
        T item;
        if (_items.Count > 0)
        {
            item = _items.Pop();
        }
        else
        {
            item = Object.Instantiate(_prefab, _parent);
            Debug.Log($"Pool<{typeof(T).Name}> expanded");
        }

        item.gameObject.SetActive(true);
        item.OnGet();
        return item;
    }

    public void Return(T item)
    {
        item.OnReturn();
        item.gameObject.SetActive(false);
        _items.Push(item);
    }
}
