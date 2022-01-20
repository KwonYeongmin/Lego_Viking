using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Countdown : MonoBehaviour
{
    Timer countdownTimer = new Timer();
    [HideInInspector]  public TMP_Text TimeTEXT;

    void StartTimer() 
    {
        countdownTimer.ResetTimer();
        countdownTimer.StartTimer();
    }

    void Update()
    {
        int countdown = 3-(int)(countdownTimer.GetTimer());
        TimeTEXT.text = countdown.ToString();

        if (countdownTimer.GetTimer() >= 3)
        {
            this.gameObject.SetActive(false);
            countdownTimer.ResetTimer();
        }
    }


}
