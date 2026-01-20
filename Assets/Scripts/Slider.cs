using TMPro;
using UnityEngine;

public class Slider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sliderValueDisplay;
    [SerializeField] private UnityEngine.UI.Slider _slider;

    private float _sliderValue;

    private void Update()
    {
        _sliderValue = _slider.value;
        _sliderValueDisplay.text = _sliderValue.ToString("0.00");
    }

    private void OnEnable()
    {
        _sliderValue = PlayerPrefs.GetFloat("aim speed", 1f);
        _sliderValueDisplay.text = _sliderValue.ToString("0.00");
        _slider.value = _sliderValue;
    }

    public void SaveAndClose()
    {
        PlayerPrefs.SetFloat("aim speed", _sliderValue);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
    }
}
