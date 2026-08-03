using System;
using UnityEngine;
using UnityEngine.UI;

public class GameplayInputView : MonoBehaviour, IGameplayInput
{
    [SerializeField] private Button _pauseButton;

    private Action _pauseHandler;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPausePressed);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPausePressed);
    }

    public void SetPauseHandler(Action handler)
    {
        _pauseHandler = handler;
    }

    private void OnPausePressed()
    {
        _pauseHandler?.Invoke();
    }
}
