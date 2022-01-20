using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Enemy_Missile, Enemy_Arrow, Enemy_Dagger, Enemy_Boss };

public class StageManager : Singleton<StageManager>
{
    public EnemySpawner enemySpawner;
    public EnemyAttackSpawner attackSpawner;
    public ItemSpawner itemSpawner; 
   //public ItemSpawner itemSpawner; //bgm

    private int Stage = 0;
    public EnemyType Type = EnemyType.Enemy_Missile;

    void Start()
    {
        Stage = 0;
        SetStage();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeStage();
        }
    }

    void ChangeStage()
    {
        Stage++;
    }

    void SetStage()
    {
        switch (Stage)
        {
            case 0: // 1-1
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile,0);
                    Type = EnemyType.Enemy_Missile;
                    attackSpawner.test("a");
                    itemSpawner.CreatibleItemIndex = 2;
                } break;
            case 1: // 1-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, 1);
                    Type = EnemyType.Enemy_Missile;
                    //attackSpawner.Instan
                    attackSpawner.test("b");
                    itemSpawner.CreatibleItemIndex = 3;
                }
                break;
            case 2: // 1-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Missile, 2);
                    Type = EnemyType.Enemy_Missile;
                    attackSpawner.test("c");
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
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, 0);
                    Type = EnemyType.Enemy_Arrow;
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 5: //2-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, 1);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 6: //2-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Arrow, 2);
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
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, 0);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 9: // 3-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, 1);
                    itemSpawner.CreatibleItemIndex = 4;
                }
                break;
            case 10: // 3-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Dagger, 2);
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
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, 0);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 13: // 4-2
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, 1);
                    itemSpawner.CreatibleItemIndex = 5;
                }
                break;
            case 14: // 4-3
                {
                    enemySpawner.InstantiateEnemy(EnemyType.Enemy_Boss, 2);
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
