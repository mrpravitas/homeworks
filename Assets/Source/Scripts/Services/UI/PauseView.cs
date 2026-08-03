using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour, IPauseView
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitToMenuButton;

    private Action _resumeHandler;
    private Action _exitHandler;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(OnResumePressed);
        _exitToMenuButton.onClick.AddListener(OnExitPressed);
    }

    private void OnDisable()
    {
        _resumeButton.onClick.RemoveListener(OnResumePressed);
        _exitToMenuButton.onClick.RemoveListener(OnExitPressed);
    }

    public void SetResumeHandler(Action handler)
    {
        _resumeHandler = handler;

        if (handler != null)
        {
            gameObject.SetActive(true);
        }
    }

    public void SetExitToMenuHandler(Action handler)
    {
        _exitHandler = handler;
    }

    private void OnResumePressed()
    {
        _resumeHandler?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnExitPressed()
    {
        _exitHandler?.Invoke();
    }
}
