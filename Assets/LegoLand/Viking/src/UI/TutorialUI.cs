using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject[] tutorialUI;
    public GameObject tutorialPanel;
    public GameObject StageInfo;
   

    int index = 0;
    bool bBtnDown = false;

    public void Start()
    {
        index = 0;
        
    }

    public void TutorialNextBtnDown()
    {
        bBtnDown = true;
        SoundManager.Instance.PlayUIAudio(SoundList.Sound_button);
    }

    private void Update()
    {
        if (bBtnDown)
        {
            if (index < 3)
            {
                tutorialUI[index].SetActive(false);
                tutorialUI[index + 1].SetActive(true);
                index++;
            }
            else
            {
                tutorialUI[index].SetActive(false);
                tutorialPanel.gameObject.SetActive(false);
                StageInfo.GetComponent<StageInfoUI>().ShowStageInfo();
            }
            bBtnDown = false;
        }

       // if (tutorialPanel.activeSelf) Time.timeScale = 0.0f;
        // else Time.timeScale = 1;
    }
       
    
}
