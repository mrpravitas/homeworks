using System;
using UnityEngine;
using UnityEngine.UI;

public class SwitchInputButton : MonoBehaviour, ISwithInput
{
    [SerializeField] private Button _button;

    private Action _handler;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveListener(HandleClick);
    }

    public void SetHandler(Action hadler)
    {
        _handler = hadler;
    }

    private void HandleClick()
    {
        _handler?.Invoke();
    }
}
