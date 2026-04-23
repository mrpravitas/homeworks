using UnityEngine;
using UnityEngine.UI;

public class LogButton : MonoBehaviour
{
    [SerializeField] private Button _button;

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
        EventBus.Raise(new GameEvent(EventType.LogRequested));
    }
}
