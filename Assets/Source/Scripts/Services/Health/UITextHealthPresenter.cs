using TMPro;
using UnityEngine;

public class UITextHealthPresenter : MonoBehaviour, IHealthPresenter
{
    [SerializeField] private TextMeshProUGUI _text;

    public void OnHealthChanged(int current, int max)
    {
        _text.text = $"{current}/{max}";
    }
}
