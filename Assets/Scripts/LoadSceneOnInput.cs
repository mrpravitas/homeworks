using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void LoadScene(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
