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

    [Header("UI")]
    [SerializeField] private GameObject _collectButton;

    private bool _hasKey;
    private int _chestsFound;
    private Pickup _currentPickup;

    private void Start()
    {
        KeyStateChanged?.Invoke(_hasKey);
        ChestsRemainingChanged?.Invoke(_chestsToFind);

        if (_collectButton != null)
            _collectButton.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Pickup pickup = other.GetComponentInParent<Pickup>();
        if (pickup == null)
            return;

        _currentPickup = pickup;

        if (_collectButton != null)
            _collectButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Pickup pickup = other.GetComponentInParent<Pickup>();
        if (pickup == null)
            return;

        if (_currentPickup == pickup)
        {
            _currentPickup = null;

            if (_collectButton != null)
                _collectButton.SetActive(false);
        }
    }

    public void Collect()
    {
        if (_currentPickup == null)
            return;

        Pickup pickup = _currentPickup;
        _currentPickup = null;

        if (_collectButton != null)
            _collectButton.SetActive(false);

        if (pickup._type == PickupType.Key)
        {
            _hasKey = true;
            KeyStateChanged?.Invoke(true);
            Destroy(pickup.gameObject);
            return;
        }

        if (pickup._type == PickupType.Chest)
        {
            if (!_hasKey)
                return;

            _chestsFound++;

            int remaining = Mathf.Max(0, _chestsToFind - _chestsFound);
            ChestsRemainingChanged?.Invoke(remaining);

            Destroy(pickup.gameObject);

            if (remaining == 0)
            {
                Invoke(nameof(RestartScene), _restartDelay);
            }

            return;
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
