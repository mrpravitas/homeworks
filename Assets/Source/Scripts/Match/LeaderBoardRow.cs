using TMPro;
using UnityEngine;

public class LeaderBoardRow : MonoBehaviour
{
    [SerializeField] private TMP_Text _nickname;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _kills;
    [SerializeField] private TMP_Text _deaths;

    public void Set(PlayerScoreEntry entry)
    {
        _nickname.text = entry.Nickname;
        _score.text = entry.Score.ToString();
        _kills.text = entry.Kills.ToString();
        _deaths.text = entry.Deaths.ToString();
    }

    public void SetHeader()
    {
        _nickname.text = "Nickname";
        _score.text = "Score";
        _kills.text = "Kills";
        _deaths.text = "Deaths";
    }
}
