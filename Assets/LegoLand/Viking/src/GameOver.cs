using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
    }

    public void ButtonReStartDown()
    {
        SceneManagement.Instance.ChangeScene("GameScene");
    }

    public void ButtonExitDown()
    {
        SceneManagement.Instance.QuitApplication();
    }
}
