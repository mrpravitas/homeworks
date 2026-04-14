using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

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
        if (gameEvent.EventType != EventType.ScoreChanged)
        {
            return;
        }

        int score = (int)gameEvent.Parameter;
        _scoreText.text = score.ToString();
    }
}
