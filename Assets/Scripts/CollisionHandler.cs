using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip finishSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollible = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.isPressed)
        {
            Debug.Log("Cheat Activated: Next Level Loaded");
            LoadNextLevel();
        }
        else if (Keyboard.current.rKey.isPressed)
        {
            Debug.Log("Cheat Activated: Level Reloaded");
            ReloadLevel();
        }
        else if (Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("Cheat Activated: Collisions Toggled");
            isCollible = !isCollible;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollible) { return; }
        switch (collision.gameObject.tag)
        {
            case "Landingpad":
                Debug.Log("Ayo Mulai Explorasi");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartFinishSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSFX);
        finishParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();

        GetComponent<Movement>().enabled = false;

        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneIndex);

        PlayerPrefs.SetInt("CurrentSceneIndex", SceneManager.GetActiveScene().buildIndex);
        int currentSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex");
        Debug.Log("from collision handler");
        Debug.Log("Current Scene Index: " + currentSceneIndex);
        SceneManager.LoadScene("Crashed");
    }
}