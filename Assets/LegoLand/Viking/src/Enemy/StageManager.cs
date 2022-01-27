using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageManager : Singleton<StageManager>
{
    public StageManager() { }

    private EnemySpawner enemySpawner;
    private EnemyAttackSpawner attackSpawner;
    private ItemSpawnerSelector itemSpawner;
    private SceneManagement sceneManager;
    private TimerUI Timer;
    private StageInfoUI StageInfo;//= FindObjectOfType<StageInfoUI>();
    [HideInInspector]public Viking viking;

    public int Stage;
    public EnemyType Type = EnemyType.Enemy_Missile;
    public EnemyColorType ColorType = EnemyColorType.GREY;

    float Speed = 0;

    private void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        itemSpawner = GameObject.Find("ItemSpawnerSelector").GetComponent<ItemSpawnerSelector>();
        viking = GameObject.Find("Pin").GetComponent<Viking>();
        sceneManager = GameObject.Find("SceneManagement").GetComponent<SceneManagement>();
        Timer = GameObject.Find("Time").GetComponent<TimerUI>();
        StageInfo = GameObject.Find("StageInfo ").GetComponent <StageInfoUI>();

        Speed = viking.speed;
    }



    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title") Stage = 0;
        if (SceneManager.GetActiveScene().name == "EndGame") Stage = 0;
    }

    private List<GameObject> enemies = new List<GameObject>();
    public List<int> enemiesAttacklist = new List<int>();

    public void AddEnemies(GameObject obj)
    {
        enemies.Add(obj);
    }

    public void RemoveEnemies(GameObject obj)
    {
        enemies.Remove(obj);

        if (obj.GetComponent<Enemy>().ColorType == EnemyColorType.GREY)
            enemiesAttacklist.Add(0);
        else if (obj.GetComponent<Enemy>().ColorType == EnemyColorType.BLUE)
            enemiesAttacklist.Add(1);
        else if (obj.GetComponent<Enemy>().ColorType == EnemyColorType.YELLOW)
            enemiesAttacklist.Add(2);
    }

    public void ChangeStage()
    {
        SavePrevTimer();

        if (Stage % 4 != 3)
        { 
            Stage++;
            SetStage();
        }
        else if (Stage % 4 == 3)
        {
            if (enemies.Count <= 0)
            {
                ClearEnemyHUD();
                
                if (Stage == 15)
                {
                    sceneManager.ChangeScene("GameOver");
                    Stage = 0;
                }
                Stage++;
                SetStage();
            }
        }

        Debug.Log("Stage: " + Stage);
    }

    private void ClearEnemyHUD()
    {
        // List<GameObject> enemyHUD_list = new List<GameObject>();
        GameObject[]  enemyHUD;
        enemyHUD= GameObject.FindGameObjectsWithTag("EnemyHUD");
        Debug.Log("enemyHUD_list : " + enemyHUD.Length);
        for (int i = 0; i < enemyHUD.Length; i++)
        {
            Destroy(enemyHUD[i]);
        }
    }

    public void SavePrevTimer()
    {
        StageInfo.SavePrevTimer(Timer.GetRec());
    }

    public void SetStage()
    {
        //
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        itemSpawner = GameObject.Find("ItemSpawnerSelector").GetComponent<ItemSpawnerSelector>();
        viking = GameObject.Find("Pin").GetComponent<Viking>();
        sceneManager = GameObject.Find("SceneManagement").GetComponent<SceneManagement>();
        Timer = GameObject.Find("Time").GetComponent<TimerUI>();
        StageInfo = GameObject.Find("StageInfo ").GetComponent<StageInfoUI>();
        
        

        /*
        Player player = GameObject.Find("Player").GetComponent<Player>();//
        player.Reset();
        */
        viking.Reset();
        //
       // itemSpawner.Reset();
        //
        enemySpawner.Reset();

        
        // Stage 정보
        if (StageInfo) StageInfo.SetStageInfo(Stage);

        if (Stage == 0)
        {
            GameObject tutorial = FindObjectOfType<TutorialUI>().gameObject;
            if (tutorial)
            {
                tutorial.GetComponent<TutorialUI>().tutorialPanel.SetActive(true);
                tutorial.GetComponent<TutorialUI>().ShowTutorial();
            }
        }
        else if(Stage !=0  )
        {
            if (StageInfo) StageInfo.ShowClearInfo();
        }


        //Timer
        Timer.StartTimer();


        switch (Stage)
        {
            case 0: // 1-1
                {
                    if(enemiesAttacklist.Count>=3)  for (int i = 0; i < 3; i++) enemiesAttacklist.Remove(i);
                    Type = EnemyType.Enemy_Missile;
                    ColorType = EnemyColorType.GREY;
                    itemSpawner.CreatibleItemIndex = 2;
                    SoundManager.Instance.PlayBGM(SoundList.Stage1);
                }
                break;
            case 1: // 1-2
                {  
                    Type = EnemyType.Enemy_Missile;
                    ColorType = EnemyColorType.BLUE;
                    itemSpawner.CreatibleItemIndex = 3;
                    viking.speed = Speed * 1.1f;
                }
                break;
            case 2: // 1-3
                {
                    Type = EnemyType.Enemy_Missile;
                    ColorType = EnemyColorType.YELLOW;
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.2f;
                }
                break;
            case 3: // 1-4
                {
                    Type = EnemyType.Enemy_Missile;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.4f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage1_Boss);
                }
                break;
            case 4: //2-1
                {
                    if (enemiesAttacklist.Count >= 3) for (int i = 0; i < 3; i++) enemiesAttacklist.Remove(i);
                    Type = EnemyType.Enemy_Arrow;
                    ColorType = EnemyColorType.GREY;
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.1f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage2);
                }
                break;
            case 5: //2-2
                {
                  
                    Type = EnemyType.Enemy_Arrow;
                    ColorType = EnemyColorType.BLUE;
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.2f;
                }
                break;
            case 6: //2-3
                {
                    Type = EnemyType.Enemy_Arrow;
                    ColorType = EnemyColorType.YELLOW;
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.3f;
                }
                break;
            case 7: //2-4
                {
                    Type = EnemyType.Enemy_Arrow;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.5f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage2_Boss);

                }
                break;
            case 8: // 3-1
                {
                    if (enemiesAttacklist.Count >= 3) for (int i = 0; i < 3; i++) enemiesAttacklist.Remove(i);
                    Type = EnemyType.Enemy_Dagger;
                    ColorType = EnemyColorType.GREY;
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.2f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage3);
                  
                }
                break;
            case 9: // 3-2
                {
                    Type = EnemyType.Enemy_Dagger;
                    ColorType = EnemyColorType.BLUE;
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.3f;
                }
                break;
            case 10: // 3-3
                {
                    Type = EnemyType.Enemy_Dagger;
                    ColorType = EnemyColorType.YELLOW;
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.4f;
                }
                break;
            case 11: // 3-4
                {
                    Type = EnemyType.Enemy_Dagger;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.6f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage3_Boss);
                }
                break;
            case 12: // 4-1
                {
                    if (enemiesAttacklist.Count >= 3) for (int i = 0; i < 3; i++) enemiesAttacklist.Remove(i);
                    Type = EnemyType.Enemy_Boss;
                    ColorType = EnemyColorType.GREY;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.3f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage4);
                  
                }
                break;
            case 13: // 4-2
                {
                    Type = EnemyType.Enemy_Boss;
                    ColorType = EnemyColorType.BLUE;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.4f;
                }
                break;
            case 14: // 4-3
                {
                    Type = EnemyType.Enemy_Boss;
                    ColorType = EnemyColorType.YELLOW;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.5f;
                }
                break;
            case 15: // 4-4
                {
                    Type = EnemyType.Enemy_Boss;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.7f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage4_Boss);
                }break;
        }


        //EnemySapwner
        if (Stage % 4 == 3) // 스테이지 마지막 레벨이면
            enemySpawner.InstantiateEnemies();
        else
            enemySpawner.InstantiateEnemy();
    }
}
