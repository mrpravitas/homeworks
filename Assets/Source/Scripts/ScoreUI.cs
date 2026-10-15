using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetScore(int score)
    {
        _text.text = score.ToString();
        Canvas.ForceUpdateCanvases();
    }
}
