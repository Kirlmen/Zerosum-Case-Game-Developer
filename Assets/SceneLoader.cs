using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void OnEnable()
    {
        Instance = this;
        LoadLastScene();
    }

    private void Awake()
    {
        GetLastScene();
    }

    [SerializeField] private int currentScene;
    [SerializeField] private int nextScene;
    private void GetLastScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = currentScene + 1;

        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", currentScene);
        }

    }

    private void LoadLastScene()
    {
        int lastScene = PlayerPrefs.GetInt("Level");
        SceneManager.LoadScene(lastScene);
    }

    public void LoadNextLevel()
    {
        if (PlayerPrefs.HasKey("Level") && PlayerPrefs.GetInt("Level") < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level") + nextScene);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("Level", currentScene);
    }


}
