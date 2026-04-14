using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinConfig _config;

    private Coroutine _respawnCoroutine;
    private Renderer _renderer;
    private Collider _collider;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        EventBus.OnGameEvent += HandleEvent;
    }

    private void OnDisable()
    {
        EventBus.OnGameEvent -= HandleEvent;
    }

    private void HandleEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == EventType.GameWon)
        {
            if (_respawnCoroutine != null)
            {
                StopCoroutine(_respawnCoroutine);
                _respawnCoroutine = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        EventBus.Raise(new GameEvent(EventType.ItemPicked));

        _respawnCoroutine = StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        ToggleView(false);

        yield return new WaitForSeconds(_config.RespawnDelay);

        ToggleView(true);

        _respawnCoroutine = null;
    }

    private void ToggleView(bool isVisible)
    {
        _renderer.enabled = isVisible;
        _collider.enabled = isVisible;
    }
}
