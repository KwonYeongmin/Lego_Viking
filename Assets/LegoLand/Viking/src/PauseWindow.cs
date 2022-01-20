using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    [HideInInspector]
    public bool isButtonPause;
    [SerializeField] private GameObject PauseMenu;
    private SceneManagement sceneManagement;

    void Start()
    {
        PauseMenu.SetActive(false);
        sceneManagement = FindObjectOfType<SceneManagement>();
    }

    void Update()
    {
        isButtonPause = Input.GetKeyDown(KeyCode.P);
    }

    public void ButtonPauseDown()
    {
        isButtonPause = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ButtonContinueDown()
    {
        isButtonPause = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ButtonReStartDown()
    {
        Time.timeScale = 1.0f;
        sceneManagement.ChangeScene("GameScene");
    }

    public void ButtonGiveUpDown()
    {
        Time.timeScale = 1.0f;
        sceneManagement.ChangeScene("GameOver");
    }
}
