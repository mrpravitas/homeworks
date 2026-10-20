using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        ScoreCounter.OnScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        ScoreCounter.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _text.SetText("{0}", score);
    }
}
