using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void LoadGamePlay()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadInnovation()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
