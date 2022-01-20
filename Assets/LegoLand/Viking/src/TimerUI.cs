using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [HideInInspector]public TMP_Text TimeTEXT;
    Timer timer = new Timer();

    public int Min { get; private set; }
    public int Sec { get; private set; }

    void Start()
    {
        Min =0;
        Sec = 0;
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
        Min = (int)(timer.GetTimer() / 60 % 60);
        Sec = (int)(timer.GetTimer() % 60);

        if(Min <10 && Sec<10) TimeTEXT.text = string.Format("0{0} : 0{1}", Min,Sec);
        else if(Min < 10 && Sec>=10) TimeTEXT.text = string.Format("0{0} : {1}", Min,Sec);
    }
}
