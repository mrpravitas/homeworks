using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadSceneOnInput : MonoBehaviour
{
    public void OpenMenuScene(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ReloadScene(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
