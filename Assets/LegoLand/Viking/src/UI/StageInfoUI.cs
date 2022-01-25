using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class StageInfoUI : MonoBehaviour
{
    public GameObject StageInfo;
    [SerializeField] private GameObject Clear;
    [SerializeField] private GameObject CountDown; // text
    [SerializeField] private GameObject Tutorial; // text
    [SerializeField] private GameObject PauseWindow; // text


    [Header("Stage Infomation")]
    [SerializeField] private TextMeshProUGUI stage_num;
    [SerializeField] private TextMeshProUGUI stage_attack;
    [SerializeField] private TextMeshProUGUI stage_enemy;
    [SerializeField] private float DelayTime;


    //
    bool bIsPlaying = true;

    public List<Stagetext> stageText;
    private void Awake()
    {
        stageText = new List<Stagetext>();
        ReadTextFile();
        SetStageInfo(0);
    }

    private void Start()
    {

       
    }

    void Update()
    {

        if (bIsPlaying) Time.timeScale = 1;
        else Time.timeScale = 0;

        if (StageInfo.activeSelf || Clear.activeSelf || Tutorial.activeSelf || CountDown.activeSelf || PauseWindow.activeSelf) bIsPlaying = false;
        else if(!StageInfo.activeSelf && !Clear.activeSelf && !Tutorial.activeSelf && !CountDown.activeSelf && !PauseWindow.activeSelf)  bIsPlaying = true;

        if (StageInfo.activeSelf)
        {
            UpdateTimer();
        }

        if (TimeOut)
        {
            StageInfo.SetActive(false);
            CountDown.SetActive(true);
            CountDown.GetComponent<Countdown>().StartTimer();
            TimeOut = false;
        }
    }


    void ReadTextFile()
    {
        stageText.Clear();

        TextAsset textFile = Resources.Load("StageInfo") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine(); 

            if (line == null) break;

            Stagetext stageData = new Stagetext();
            stageData.stage_num = line.Split(',')[0];
            stageData.stage_attack = line.Split(',')[1];
            stageData.stage_enemy = line.Split(',')[2];
            stageText.Add(stageData);
        }
        stringReader.Close();
    }

    public void ShowStageInfo()
    {
        Debug.Log("ShowStageInfo");
        StageInfo.SetActive(true);
        StartTimer();
    }

    public void SetStageInfo(int index)
    {
        stage_num.text = "Stage " + stageText[index].stage_num;
        stage_attack.text = stageText[index].stage_attack;
        stage_enemy.text = stageText[index].stage_enemy;
    }


// ================= timer ===================

    private float timer = 0;
    private bool TimeOut = false;
    private bool bTimer = false;

    private void StartTimer()
    {
        timer = 0; //reset
        bTimer = true;
    }

    private void UpdateTimer()
    {
        if (bTimer)
            timer += Time.unscaledDeltaTime;

        if (timer >= DelayTime) TimeOut = true;
    }
    
}
