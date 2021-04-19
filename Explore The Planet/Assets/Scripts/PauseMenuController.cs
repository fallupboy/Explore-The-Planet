using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuUI;

    void Awake()
    {
        int numPauseMenuControllers = FindObjectsOfType<PauseMenuController>().Length;
        if (numPauseMenuControllers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu" || SceneManager.GetActiveScene().name == "Congratulations")
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControlPauseMenu();
        }
    }

    public void ControlPauseMenu()
    {
        if (!pauseMenuUI.activeSelf)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;

        }
    }

    public void LoadMainMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
