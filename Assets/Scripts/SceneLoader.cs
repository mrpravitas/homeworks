using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OpenMenuScene()
    {
        LoadMenu();
    }

    public void OpenMenuScene(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            LoadMenu();
        }
    }

    public void ReloadScene(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void LoadMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
