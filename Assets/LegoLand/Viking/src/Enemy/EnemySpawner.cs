using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies_Missile;
    public GameObject[] Enemies_Arrow;
    public GameObject[] Enemies_Dagger;
    public GameObject[] Enemies_Boss;

    private GameObject enemyPrefab;
    private GameObject enemy;
    Transform transform_;

    private void Start()
    {
        transform_ = GameObject.Find("enemyPoint").transform;
        if (transform_) Debug.Log("찾음");
        else Debug.Log("못찾음");
    }

    public void InstantiateEnemies(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Enemy_Missile:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transform_.position.x + 10 * (i - 1), transform_.position.y, transform_.position.z);
                        enemy = Instantiate(Enemies_Missile[i], pos, transform_.rotation);
                        enemy.transform.parent = transform_;
                    }
                }
                break;
            case EnemyType.Enemy_Arrow:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transform_.position.x + 10 * (i - 1), transform_.position.y, transform_.position.z);
                        enemy = Instantiate(Enemies_Arrow[i], pos, transform_.rotation);
                        enemy.transform.parent = transform_;
                    }
                }
                break;
            case EnemyType.Enemy_Dagger:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transform_.position.x + 10 * (i - 1), transform_.position.y, transform_.position.z);
                        enemy = Instantiate(Enemies_Dagger[i], pos, transform_.rotation);
                        enemy.transform.parent = transform_;
                    }
                }
                break;
            case EnemyType.Enemy_Boss:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transform_.position.x + 10 * (i - 1), transform_.position.y, transform_.position.z);
                        enemy = Instantiate(Enemies_Boss[i], pos, transform_.rotation);
                        enemy.transform.parent = transform_;
                    }
                }
                break;
        }
    }
    public void InstantiateEnemy(EnemyType type,int n)
    {
        switch (type)
        {
            case EnemyType.Enemy_Missile: { enemyPrefab = Enemies_Missile[n]; } break;
            case EnemyType.Enemy_Arrow: { enemyPrefab = Enemies_Arrow[n]; } break;
            case EnemyType.Enemy_Dagger: { enemyPrefab = Enemies_Dagger[n]; } break;
            case EnemyType.Enemy_Boss: { enemyPrefab = Enemies_Boss[n]; } break;
        }

        if (n != 3)
        {
            enemy = Instantiate(enemyPrefab, transform_);
            enemy.transform.parent = transform_;
        }
    }


}
