using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : Component, IPoolable
{
    private readonly T _prefab;
    private readonly Stack<T> _items = new();

    public Pool(T prefab, int initialSize)
    {
        _prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            T item = Object.Instantiate(_prefab);
            item.OnReturn();
            item.gameObject.SetActive(false);
            _items.Push(item);
        }
    }

    public T Get()
    {
        T item = _items.Count > 0 ? _items.Pop() : Object.Instantiate(_prefab);
        item.gameObject.SetActive(true);
        item.OnGet();
        return item;
    }

    public void Release(T item)
    {
        item.OnReturn();
        item.gameObject.SetActive(false);
        _items.Push(item);
    }
}
