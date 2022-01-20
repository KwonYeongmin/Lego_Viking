using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageManager : Singleton<StageManager>
{
    public StageManager() { }

    private EnemySpawner enemySpawner;
    private EnemyAttackSpawner attackSpawner;
    private ItemSpawner itemSpawner;
    private SceneManagement sceneManager;
    private TimerUI Timer;
    private Viking viking;
    //public ItemSpawner itemSpawner; //bgm

    public int Stage;// { get; private set; }
   // public int Index { get; private set; }
    public EnemyType Type = EnemyType.Enemy_Missile;
    public EnemyColorType ColorType = EnemyColorType.GREY;

    float Speed = 5;

    private void Awake()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        viking = GameObject.Find("Pin").GetComponent<Viking>();
        sceneManager = GameObject.Find("SceneManagement").GetComponent<SceneManagement>();
        Speed = viking.speed;
    }

    void Start()
    {
        //  Stage = 0;
        //  SetStage();

        Stage = 0;
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
        if (Stage % 4 != 3)
        {

                
            Stage++;
            SetStage();
        }
        else if (Stage % 4 == 3)
        {

            if (enemies.Count <= 0)
            {
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

    public void SetStage()
    {
        Debug.Log("SetStage");
        

        switch (Stage)
        {
            case 0: // 1-1
                {
                    Type = EnemyType.Enemy_Missile;
                    ColorType = EnemyColorType.GREY;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 2;
                    viking.speed = Speed;
                    SoundManager.Instance.PlayBGM(SoundList.Stage1);
                }
                break;
            case 1: // 1-2
                {
                    Type = EnemyType.Enemy_Missile;
                    ColorType = EnemyColorType.BLUE;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, EnemyColorType.BLUE);
                    //attackSpawner.Instan
                    // attackSpawner.test("b");
                    itemSpawner.CreatibleItemIndex = 3;
                    viking.speed = Speed * 1.1f;
                }
                break;
            case 2: // 1-3
                {
                    Type = EnemyType.Enemy_Missile;
                    ColorType = EnemyColorType.YELLOW;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, EnemyColorType.YELLOW);
                    // attackSpawner.test("c");
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.2f;
                }
                break;
            case 3: // 1-4
                {
                    Type = EnemyType.Enemy_Missile;
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Missile);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.4f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage1_Boss);
                }
                break;
            case 4: //2-1
                {
                    Type = EnemyType.Enemy_Arrow;
                    ColorType = EnemyColorType.GREY;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.1f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage2);
                    for(int i=0;i<3;i++) enemiesAttacklist.Remove(i);
                }
                break;
            case 5: //2-2
                {
                  
                    Type = EnemyType.Enemy_Arrow;
                    ColorType = EnemyColorType.BLUE;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, EnemyColorType.YELLOW);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.2f;
                }
                break;
            case 6: //2-3
                {
                    Type = EnemyType.Enemy_Arrow;
                    ColorType = EnemyColorType.YELLOW;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, EnemyColorType.YELLOW);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.3f;
                }
                break;
            case 7: //2-4
                {
                    Type = EnemyType.Enemy_Arrow;
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Arrow);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.5f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage2_Boss);

                }
                break;
            case 8: // 3-1
                {
                    Type = EnemyType.Enemy_Dagger;
                    ColorType = EnemyColorType.GREY;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.2f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage3);
                    for (int i = 0; i < 3; i++) enemiesAttacklist.Remove(i);
                }
                break;
            case 9: // 3-2
                {
                    Type = EnemyType.Enemy_Dagger;
                    ColorType = EnemyColorType.BLUE;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, EnemyColorType.BLUE);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.3f;
                }
                break;
            case 10: // 3-3
                {
                    Type = EnemyType.Enemy_Dagger;
                    ColorType = EnemyColorType.YELLOW;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, EnemyColorType.YELLOW);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.4f;
                }
                break;
            case 11: // 3-4
                {
                    Type = EnemyType.Enemy_Dagger;
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Dagger);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.6f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage3_Boss);
                }
                break;
            case 12: // 4-1
                {
                    Type = EnemyType.Enemy_Boss;
                    ColorType = EnemyColorType.GREY;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.3f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage4);
                    for (int i = 0; i < 3; i++) enemiesAttacklist.Remove(i);
                }
                break;
            case 13: // 4-2
                {
                    Type = EnemyType.Enemy_Boss;
                    ColorType = EnemyColorType.BLUE;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, EnemyColorType.BLUE);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.4f;
                }
                break;
            case 14: // 4-3
                {
                    Type = EnemyType.Enemy_Boss;
                    ColorType = EnemyColorType.YELLOW;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, EnemyColorType.YELLOW);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.5f;
                }
                break;
            case 15: // 4-4
                {
                    Type = EnemyType.Enemy_Boss;
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Boss);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.7f;
                    SoundManager.Instance.PlayBGM(SoundList.Stage4_Boss);
                }
                break;
        }
    }



}
