using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Enemy_Missile, Enemy_Arrow, Enemy_Dagger, Enemy_Boss };

public class StageManager : Singleton<StageManager>
{
    private EnemySpawner enemySpawner;
    private EnemyAttackSpawner attackSpawner;
    private ItemSpawner itemSpawner; 
   //public ItemSpawner itemSpawner; //bgm

    public int Stage { get; private set; }
   // public int Index { get; private set; }
    public EnemyType Type = EnemyType.Enemy_Missile;
    public EnemyColorType ColorType = EnemyColorType.GREY;

    void Start()
    {
        Stage = 0;
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        SetStage();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeStage();
        }
    }

    public void ChangeStage()
    {
        Stage++;
        SetStage();
    }

    void SetStage()
    {
        Debug.Log("SetStage");
        switch (Stage)
        {
            case 0: // 1-1
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, EnemyColorType.GREY);
                    Type = EnemyType.Enemy_Missile;
                    //attackSpawner.test("a");
                    itemSpawner.CreatibleItemIndex = 2;
                } break;
            case 1: // 1-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, EnemyColorType.BLUE);
                    Type = EnemyType.Enemy_Missile;
                    //attackSpawner.Instan
                   // attackSpawner.test("b");
                    itemSpawner.CreatibleItemIndex = 3;
                }
                break;
            case 2: // 1-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, EnemyColorType.YELLOW);
                    Type = EnemyType.Enemy_Missile;
                   // attackSpawner.test("c");
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 3: // 1-4
                {
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Missile);
                    Type = EnemyType.Enemy_Missile;
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 4: //2-1
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, EnemyColorType.GREY);
                    Type = EnemyType.Enemy_Arrow;
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 5: //2-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, EnemyColorType.BLUE);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 6: //2-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, EnemyColorType.YELLOW);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 7: //2-4
                {
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Arrow);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 8: // 3-1
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 9: // 3-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, EnemyColorType.BLUE);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 10: // 3-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, EnemyColorType.YELLOW);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 11: // 3-4
                {
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Dagger);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 12: // 4-1
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, EnemyColorType.GREY);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 13: // 4-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, EnemyColorType.BLUE);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 14: // 4-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, EnemyColorType.YELLOW);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 15: // 4-4
                {
                    enemySpawner.InstantiateEnemies(EnemyType.Enemy_Boss);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
        }
    }



}
