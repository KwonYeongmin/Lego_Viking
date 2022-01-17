using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType Type = EnemyType.Enemy_Missile;

    public GameObject[] Enemies_Missile;
    public GameObject[] Enemies_Arrow;
    public GameObject[] Enemies_Dagger;
    public GameObject[] Enemies_Boss;

    private GameObject enemy;

    private int InStageNum = 0;


    void Start()
    {
        SetEnemy();
        InstantiateEnemy();
    }


    public void SetEnemy()
    { 
        Type = EnemyManager.Instance.Type;
        InStageNum = EnemyManager.Instance.GetInStageNum();
    }


    public void InstantiateEnemy()
    {
        switch (Type)
        {
            case EnemyType.Enemy_Missile:
                {
                    if (InStageNum != 3)
                        Instantiate(Enemies_Missile[InStageNum],this.transform).transform.parent = this.gameObject.transform;
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            enemy = Instantiate(Enemies_Missile[i]  
                                , new Vector3(this.transform.position.x + 10 * (i-1), this.transform.position.y, this.transform.position.z)
                                , this.transform.rotation);
                            enemy.transform.parent = this.gameObject.transform;
                           EnemyManager.Instance.enemies.Add(enemy);
                        }
                    }
                } break;
            case EnemyType.Enemy_Arrow:
                {
                    if (InStageNum != 3)
                        Instantiate(Enemies_Arrow[InStageNum]).transform.parent = this.gameObject.transform;
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            enemy = Instantiate(Enemies_Arrow[i]
                                , new Vector3(this.transform.position.x + 10 * (i - 1), this.transform.position.y, this.transform.position.z)
                                , this.transform.rotation);
                            enemy.transform.parent = this.gameObject.transform;
                            EnemyManager.Instance.enemies.Add(enemy);
                        }
                    }
                } break;
            case EnemyType.Enemy_Dagger:
                {
                    if (InStageNum != 3)
                        Instantiate(Enemies_Dagger[InStageNum]).transform.parent = this.gameObject.transform;
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            enemy = Instantiate(Enemies_Dagger[i]
                                , new Vector3(this.transform.position.x + 10 * (i - 1), this.transform.position.y, this.transform.position.z)
                                , this.transform.rotation);
                            enemy.transform.parent = this.gameObject.transform;
                            EnemyManager.Instance.enemies.Add(enemy);
                        }
                    }
                } break;
            case EnemyType.Enemy_Boss:
                {
                    if (InStageNum != 3)
                        Instantiate(Enemies_Boss[InStageNum]).transform.parent = this.gameObject.transform;
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            enemy = Instantiate(Enemies_Boss[i]
                                , new Vector3(this.transform.position.x + 10 * (i - 1), this.transform.position.y, this.transform.position.z)
                                , this.transform.rotation);
                            enemy.transform.parent = this.gameObject.transform;
                            EnemyManager.Instance.enemies.Add(enemy);
                        }
                    }
                } break;
        }
        
    }
}
