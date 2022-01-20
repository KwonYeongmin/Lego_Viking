using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{

    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}