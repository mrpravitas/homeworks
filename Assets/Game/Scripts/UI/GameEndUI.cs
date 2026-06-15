using TMPro;
using UnityEngine;

public class GameEndUI : MonoBehaviour
{
    [Header("Win")]
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private TextMeshProUGUI _winScoreText;

    [Header("Lose")]
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private TextMeshProUGUI _loseScoreText;

    private int _finalScore;

    private void OnEnable()
    {
        GameStateMachine.OnGameStateChanged += HandleStateChanged;
        ScoreCounter.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        GameStateMachine.OnGameStateChanged -= HandleStateChanged;
        ScoreCounter.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        _finalScore = score;
    }

    private void HandleStateChanged(GameState state)
    {
        if (state == GameState.Win)
        {
            Time.timeScale = 0f;
            _winScreen.SetActive(true);
            _winScoreText.text = $"Score: {_finalScore}";
        }
        else if (state == GameState.Lose)
        {
            Time.timeScale = 0f;
            _loseScreen.SetActive(true);
            _loseScoreText.text = $"Score: {_finalScore}";
        }
    }
}
