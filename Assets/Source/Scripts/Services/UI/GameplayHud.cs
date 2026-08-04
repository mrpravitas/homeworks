using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayHud : MonoBehaviour, IGameplayHud
{
    [SerializeField] private TextMeshProUGUI _modifiersLabel;

    public void ShowModifiers(List<IGameModifier> modifiers)
    {
        if (modifiers == null || modifiers.Count == 0)
        {
            _modifiersLabel.text = "No modifiers";
            return;
        }

        _modifiersLabel.text = string.Join("\n", modifiers.Select(m => m.GetType().Name));
    }

    public void HideModifiers()
    {
        _modifiersLabel.text = "";
    }
}
