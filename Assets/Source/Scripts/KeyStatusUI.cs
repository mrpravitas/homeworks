using TMPro;
using UnityEngine;

public class KeyStatusUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _label;

    private void OnEnable()
    {
        PlayerCollect.KeyStateChanged += SetKeyState;
    }

    private void OnDisable()
    {
        PlayerCollect.KeyStateChanged -= SetKeyState;
    }

    private void SetKeyState(bool hasKey)
    {
        _label.text = hasKey ? "Key: yes" : "Key: no";
    }
}
