using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEOVERUI : MonoBehaviour
{
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        if (UnityEngine.Application.isEditor)
            Debug.LogError("OH NO! I VANNOTR QUIT IN EDITUR!");
        else
            UnityEngine.Application.Quit();
    }
}
