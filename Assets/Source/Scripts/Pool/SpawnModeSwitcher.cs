using System;
using UnityEngine;

public class SpawnModeSwitcher : MonoBehaviour
{
    [SerializeField] private KeyCode _key;

    private SpawnMode _mode = SpawnMode.Native;

    public static event Action<SpawnMode> ModeChanged;

    private void Start()
    {
        Debug.Log($"Spawn mode switch key: {_key}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            _mode = _mode == SpawnMode.Native ? SpawnMode.Pooled : SpawnMode.Native;
            ModeChanged?.Invoke(_mode);
        }
    }
}
