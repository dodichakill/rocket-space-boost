using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ReloadLevel()
    {
        // Reload the current scene
        // buatkan code untuk reload level sebelum ini
        int currentSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex");
        Debug.Log("Current Scene Index: " + currentSceneIndex);
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
    }
    public void Paused()
    {
        // Pause the game
        Time.timeScale = 0f;
        Debug.Log("Game is paused");
    }

    public void Resume()
    {
        // Resume the game
        Time.timeScale = 1f;
        Debug.Log("Game is resumed");
    }
}