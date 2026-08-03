using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviour, IGameOverView
{
    [SerializeField] private Button _restartButton;

    private Action _restartHandler;

    public void SetRestartHandler(Action handler)
    {
        _restartHandler = handler;

        if (handler != null)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartPressed);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartPressed);
    }

    private void OnRestartPressed()
    {
        _restartHandler?.Invoke();
    }
}
