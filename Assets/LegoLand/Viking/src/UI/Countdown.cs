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
    [HideInInspector]public bool bIsEnded;

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
            timer -= Time.unscaledDeltaTime;
            TimeTEXT.text = ((int)timer + 1).ToString();
        }
        else if (!bIsEnded)
        {
            EndTimer();
            this.gameObject.SetActive(false); // 3,2,1,0 ������ �� ���� ������Ʈ �Ⱥ��̱�
        }
    }

    private void EndTimer()
    {
        timer = 0;
        TimeTEXT.text = ((int)timer).ToString();
        bIsEnded = true;
       //  StageManager.Instance.ChangeStage();
    }

    public void StartTimer() //���⼭ �������ָ� ��
    {
        timer = MaxTime;
        TimeTEXT.text = ((int)timer).ToString();
        bIsEnded = false;
    }
}




