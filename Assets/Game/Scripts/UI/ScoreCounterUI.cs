using System.Text;
using TMPro;
using UnityEngine;

public class ScoreCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private StringBuilder _stringBuilder;

    private void Awake()
    {
        _stringBuilder = new StringBuilder();
    }

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
        _stringBuilder.Clear();
        _stringBuilder.Append("Score: ");
        _stringBuilder.Append(score);
        _text.text = _stringBuilder.ToString();
    }
}
