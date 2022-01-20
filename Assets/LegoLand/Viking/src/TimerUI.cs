using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    private TMP_Text TimeTEXT;
    Timer timer = new Timer();

    void Start()
    {
       
    }

    void Update()
    {
        timer.UpdateTimer();
        UpdateUI();
    }

    void StartTimer()
    {
        timer.StartTimer();
    }

    private void UpdateUI()
    {

        TimeTEXT.text = string.Format("{0} : {1}",0, timer.GetTimer()/60.0f);
    }
}
