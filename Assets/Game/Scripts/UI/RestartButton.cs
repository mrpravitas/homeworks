using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(RestartScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(RestartScene);
    }

    private void RestartScene()
    {
        Scene curentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(curentScene.buildIndex);
    }
}
