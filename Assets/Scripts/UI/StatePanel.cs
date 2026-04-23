using TMPro;
using UnityEngine;

public class StatePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statePanel;

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
        if (gameEvent.EventType != EventType.GameStateChanged)
        {
            return;
        }

        UpdateState((GameState)gameEvent.Parameter);
    }

    private void UpdateState(GameState gameState)
    {
        _statePanel.text = $"Current state: {gameState}";
    }
}
