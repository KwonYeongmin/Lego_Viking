using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [HideInInspector]public AudioClip title;
   [HideInInspector] public AudioClip btnSFX;

    private void Awake()
    {
        SoundManager.Instance.PlayBGM(title);   
    }

    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void PlayBtnSFX()
    {
        SoundManager.Instance.PlayUIAudio(btnSFX);
    }
}
