using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class InterfaceTimer : MonoBehaviour
{
    [Header("Interface GameObject")]
    [SerializeField] private GameObject StageInfo;
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private GameObject Clear;
    [SerializeField] private Countdown countdown;

    [Header("Interface DelayTime")]
    [SerializeField]private float DelayTime = 3.0f;
    public bool isPlay = false;

    [Header("Stage Infomation")]
    [SerializeField] private TextMeshProUGUI stage_num;
    [SerializeField] private TextMeshProUGUI stage_attack;
    [SerializeField] private TextMeshProUGUI stage_enemy;

    public List<Stagetext> stageText;

    [Header("Clear Time")]
    [SerializeField] private TextMeshProUGUI clear_time;
    [SerializeField] private TextMeshProUGUI timer;

    private void Awake()
    {
        StageInfo.SetActive(false);
        Tutorial.SetActive(false);
        Clear.SetActive(false);

        stageText = new List<Stagetext>();

        ReadTextFile();

        StageInfoSetup(0);
    }

    private void ReadTextFile()
    {
        stageText.Clear();

        TextAsset textFile = Resources.Load("StageInfo") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
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

    public void StageInfoSetup(int num)
    {
        isPlay = false;
        StageInfo.SetActive(true);
        stage_num.text = "Stage " + stageText[num].stage_num;
        stage_attack.text = stageText[num].stage_attack;
        stage_enemy.text = stageText[num].stage_enemy;
        StartCoroutine(OFF_StageInfo());
    }

    public void ClearTimeSetup()
    {
        isPlay = false;
        Clear.SetActive(true);
        clear_time.text = timer.text;
        StartCoroutine(OFF_Clear());
    }

    IEnumerator OFF_StageInfo()
    {
        yield return new WaitForSeconds(DelayTime);
        StageInfo.SetActive(false);
        if(StageManager.Instance.Stage < 4)
        {
            Tutorial.SetActive(true);
            StartCoroutine(OFF_StageTutorial());
        }
        else
        {
            countdown.gameObject.SetActive(true);
            countdown.StartTimer();
        }
    }

    IEnumerator OFF_StageTutorial()
    {
        yield return new WaitForSeconds(DelayTime);
        Tutorial.SetActive(false);
        countdown.gameObject.SetActive(true);
        countdown.StartTimer();
    }

    IEnumerator OFF_Clear()
    {
        yield return new WaitForSeconds(DelayTime);
        Clear.SetActive(false);
        StageInfoSetup(StageManager.Instance.Stage);
    }
}
