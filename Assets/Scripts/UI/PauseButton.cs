using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _pauseScreen;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClicked);
    }

    private void OnClicked()
    {
        EventBus.Raise(new GameEvent(EventType.GamePaused));
        TogglePauseScreen();
    }

    private void TogglePauseScreen()
    {
        _pauseScreen?.SetActive(!_pauseScreen.activeSelf);
    }
}
