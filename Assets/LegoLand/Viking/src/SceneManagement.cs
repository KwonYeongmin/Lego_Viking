using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : Singleton<SceneManagement>
{
    protected SceneManagement(){}

    public AudioClip title;

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(title);   
    }

    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
