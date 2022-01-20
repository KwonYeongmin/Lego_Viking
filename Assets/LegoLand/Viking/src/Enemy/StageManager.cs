using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Enemy_Missile, Enemy_Arrow, Enemy_Dagger, Enemy_Boss };

public class StageManager : Singleton<StageManager>
{
    private EnemySpawner enemySpawner;
    private EnemyAttackSpawner attackSpawner;
    private ItemSpawner itemSpawner; 
    private Viking viking;
    //public ItemSpawner itemSpawner; //bgm

    public int Stage { get; private set; }
   // public int Index { get; private set; }
    public EnemyType Type = EnemyType.Enemy_Missile;
    public EnemyColorType ColorType = EnemyColorType.GREY;


    float Speed = 5;

    private void Awake()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        viking = GameObject.Find("Pin").GetComponent<Viking>();
        Speed = viking.speed;
    }

    void Start()
    {
        Stage = 3;
        
        SetStage();
    }

    private int enemyNum = 0;

    public void ChangeStage()
    {
        if (Stage % 4 != 3)
        {
            Stage++;
            SetStage();
        }
        else
        {
            enemyNum++;
            if (enemyNum >= 3)
            {
                Stage++;
                SetStage();
            }
        }

        Debug.Log("Stage: "+Stage);
    }

    void SetStage()
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
                } break;
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
                    enemyNum = 0;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.4f;
                }
                break;
            case 4: //2-1
                {
                   
                    Type = EnemyType.Enemy_Arrow;
                    ColorType = EnemyColorType.GREY;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.1f;
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
                    enemyNum = 0;
                    viking.speed = Speed * 1.5f;
                }
                break;
            case 8: // 3-1
                {
                    Type = EnemyType.Enemy_Dagger;
                    ColorType = EnemyColorType.GREY;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 4;
                    viking.speed = Speed * 1.2f;
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
                    enemyNum = 0;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.6f;
                }
                break;
            case 12: // 4-1
                {
                    Type = EnemyType.Enemy_Boss;
                    ColorType = EnemyColorType.GREY;
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.3f;
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
                    enemyNum = 0;
                    itemSpawner.CreatibleItemIndex = 5;
                    viking.speed = Speed * 1.7f;
                }
                break;
        }
    }



}
