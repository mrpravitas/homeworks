using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        ScoreCounter.OnScoreChanged += ChangeScoreUI;
    }

    private void OnDisable()
    {
        ScoreCounter.OnScoreChanged -= ChangeScoreUI;
    }

    private void ChangeScoreUI(int score)
    {
        _text.text = $"Score: {score}";
    }
}
