using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    [HideInInspector]
    public TMP_Text TimeTEXT;

    private float timer;
    private float MaxTime = 3f;
    private bool bIsEnded;

    void Update()
    {
        if (bIsEnded)
            return;

        Check_Timer();
    }

    private void Check_Timer()
    {
        if (0 < timer)
        {
            timer -= Time.deltaTime;
            TimeTEXT.text = ((int)timer).ToString();
        }
        else if (!bIsEnded)
        {
            EndTimer();
            this.gameObject.SetActive(false); // 3,2,1,0 끝나면 이 게임 오브젝트 안보이기
        }
    }

    private void EndTimer()
    {
        timer = 0;
        TimeTEXT.text = ((int)timer).ToString();
        bIsEnded = true;
        if (StageManager.Instance.Stage == 0)
            StageManager.Instance.SetStage();
        else StageManager.Instance.ChangeStage();
    }

    public void StartTimer() //여기서 시작해주면 됨
    {
        timer = MaxTime;
        TimeTEXT.text = ((int)timer).ToString();
        bIsEnded = false;
    }
}




