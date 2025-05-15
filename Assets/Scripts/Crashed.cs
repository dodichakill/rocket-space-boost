using UnityEngine;
using UnityEngine.SceneManagement;
public class Crashed : MonoBehaviour
{
    public void ReloadLevel()
    {
        // Reload the current scene
        // buatkan code untuk reload level sebelum ini
        int currentSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex");
        Debug.Log("Current Scene Index: " + currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitApplication()
    {
        // Quit the application
        Application.Quit();
        Debug.Log("Application Quit");
    }

}
