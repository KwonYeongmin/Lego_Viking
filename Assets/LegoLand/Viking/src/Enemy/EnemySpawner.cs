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
    public Transform transform_;
    
    public EnemyType Type { get; private set; }

    private void Awake()
    {
        StageManager.Instance.gameObject.SetActive(true);
    }

    private void Start()
    {
       transform_ = GameObject.Find("enemyPoint").transform;

        StageManager.Instance.Stage = 0;
        var Interface = FindObjectOfType<InterfaceTimer>();
        if (Interface != null)
            Interface.StageInfoSetup(StageManager.Instance.Stage);
        //StageManager.Instance.SetStage();
    }

    public void InstantiateEnemies(EnemyType type)
    {
        transform_ = GameObject.Find("enemyPoint").transform;
        switch (type)
        {
            case EnemyType.Enemy_Missile:
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Vector3 pos = new Vector3(transform_.position.x + 10 * (i - 1), transform_.position.y, transform_.position.z);
                        enemy = Instantiate(Enemies_Missile[i], pos, transform_.rotation);
                        enemy.transform.parent = transform_;
                        enemy.GetComponent<Enemy>().initialized(i);
                        StageManager.Instance.AddEnemies(enemy);
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
                        enemy.GetComponent<Enemy>().initialized(i);
                        StageManager.Instance.AddEnemies(enemy);
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
                        enemy.GetComponent<Enemy>().initialized(i);
                        StageManager.Instance.AddEnemies(enemy);
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
                        enemy.GetComponent<Enemy>().initialized(i);
                        StageManager.Instance.AddEnemies(enemy);
                    }
                }
                break;
        }
    }

    public void InstantiateEnemy(EnemyType type, EnemyColorType colorType)
    {
        transform_ = GameObject.Find("enemyPoint").transform;

        switch (type)
        {
            case EnemyType.Enemy_Missile: { enemyPrefab = Enemies_Missile[(int)colorType]; } break;
            case EnemyType.Enemy_Arrow: {  enemyPrefab = Enemies_Arrow[(int)colorType]; } break;
            case EnemyType.Enemy_Dagger: {  enemyPrefab = Enemies_Dagger[(int)colorType]; } break;
            case EnemyType.Enemy_Boss: {  enemyPrefab = Enemies_Boss[(int)colorType]; } break;
        }
   
        enemy = Instantiate(enemyPrefab, transform_);
        enemy.transform.parent = transform_;
        enemy.GetComponent<Enemy>().initialized((int)colorType);

    }
}
