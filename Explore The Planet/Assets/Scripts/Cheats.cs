using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    bool collisionIsEnabled = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
            Debug.Log("NEXT LEVEL WAS LOADED!");
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            if (collisionIsEnabled)
            {
                Debug.Log("Collisions were DISABLED!");
                collisionIsEnabled = false;
            }
            else
            {
                Debug.Log("Collisions were ENABLED!");
                collisionIsEnabled = true;
            }

        }
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public bool GetCollisionState()
    {
        return collisionIsEnabled;
    }
}
