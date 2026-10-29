using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
    public static event Action<bool> KeyStateChanged;
    public static event Action<int> ChestsRemainingChanged;

    [Header("Settings")]
    [SerializeField] private int _chestsToFind;
    [SerializeField] private float _restartDelay;

    private bool _hasKey;
    private int _chestsFound;

    private void Start()
    {
        KeyStateChanged?.Invoke(_hasKey);
        ChestsRemainingChanged?.Invoke(_chestsToFind);
    }

    public bool TryCollect(PickupType type)
    {
        if (type == PickupType.Key)
        {
            _hasKey = true;
            KeyStateChanged?.Invoke(true);
            return true;
        }

        if (!_hasKey)
        {
            return false;
        }

        _chestsFound++;

        int remaining = Mathf.Max(0, _chestsToFind - _chestsFound);
        ChestsRemainingChanged?.Invoke(remaining);

        if (remaining == 0)
        {
            Invoke(nameof(RestartScene), _restartDelay);
        }

        return true;
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
