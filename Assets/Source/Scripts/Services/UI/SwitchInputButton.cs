using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class SwitchInputButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private IInputSwitcher _inputSwitcher;

    [Inject]
    public void Construct(IInputSwitcher inputSwitcher)
    {
        _inputSwitcher = inputSwitcher;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button?.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        _inputSwitcher.Switch();
    }
}
