using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour, IMainMenuView
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;

    private Action _startHandler;
    private Action _exitHandler;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartPressed);
        _exitButton.onClick.AddListener(OnExitPressed);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartPressed);
        _exitButton.onClick.RemoveListener(OnExitPressed);
    }

    public void SetStartHandler(Action handler)
    {
        _startHandler = handler;
    }

    public void SetExitHandler(Action handler)
    {
        _exitHandler = handler;
    }

    private void OnStartPressed()
    {
        _startHandler?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnExitPressed()
    {
        _exitHandler?.Invoke();
    }
}
