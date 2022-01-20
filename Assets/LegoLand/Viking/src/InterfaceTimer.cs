using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class InterfaceTimer : MonoBehaviour
{
    [Header("Interface GameObject")]
    [SerializeField] private GameObject SceneInfo;
    [SerializeField] private GameObject Tutorial;
    [SerializeField] private GameObject Clear;

    private float SceneInfoTimer = 0.0f;
    private float TutorialTimer = 0.0f;
    private float ClearTimer = 0.0f;

    [Header("Interface DelayTime")]
    [SerializeField]private float DelayTime = 3.0f;

    [Header("Stage Infomation")]
    [SerializeField] private TextMeshProUGUI stage_num;
    [SerializeField] private TextMeshProUGUI stage_attack;
    [SerializeField] private TextMeshProUGUI stage_enemy;

    public List<Stagetext> stageText;

    [Header("Clear Time")]
    [SerializeField] private TextMeshProUGUI clear_time;

    private void Awake()
    {
        SceneInfo.SetActive(false);
        Tutorial.SetActive(false);
        Clear.SetActive(false);

        stageText = new List<Stagetext>();

        ReadTextFile();
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
        stage_num.text = "Stage " + stageText[num].stage_num;
        stage_attack.text = stageText[num].stage_attack;
        stage_enemy.text = stageText[num].stage_enemy;
    }

    public void ClearTimeSetup(string time)
    {
        clear_time.text = time;
    }

    void Update()
    {
        
    }
}
