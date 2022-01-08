using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Enemy_Missile, Enemy_Arrow, Enemy_Dagger, Enemy_Boss };

public class EnemyManager : MonoBehaviour
{
    #region EnemyVariables
    public EnemyType Type = EnemyType.Enemy_Missile;
    //public EnemyType GetEnemyType() { return Type; }
    public GameObject[] Enemies;
    private int Stage = 0;
    #endregion

    #region Singleton
    private static EnemyManager sInstance;
    public static EnemyManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                GameObject newGameObj = new GameObject("_EnemyManager");
                sInstance = newGameObj.AddComponent<EnemyManager>();
            }
            return sInstance;
        }
    }

    void Awake()
    {
        sInstance = this;
    }
    #endregion

    void Start()
    {
        InstantiateEnemy();
    }

    public void ChangeStage()
    {
        OnChangeNextStage(); // 스테이지 바꾸기
        OnChangeEnemyType(); //적 타입 바꾸기 
        InstantiateEnemy(); // 적 생성
    }

    private void OnChangeNextStage()
    {
        Stage++;
    }

    public int GetStage() { return Stage; }

    void OnChangeEnemyType()
    {
        if (Stage < 3) Type = EnemyType.Enemy_Missile;
        else if (Stage >= 3 && Stage < 6) Type = EnemyType.Enemy_Arrow;
        else if (Stage >= 6 && Stage < 9) Type = EnemyType.Enemy_Dagger;
        else if (Stage >= 9 && Stage < 12) Type = EnemyType.Enemy_Boss;
    }

    void InstantiateEnemy()
    {
        if (Stage % 3 != 2) // 2, 5, 8, 11
        {
            GameObject enemy = Instantiate(Enemies[Stage], this.transform);
        }    
        else
        {
            GameObject enemy1= Instantiate(Enemies[Stage-2], 
                new Vector3(this.transform.position.x - 20.0f, this.transform.position.y, this.transform.position.z),this.transform.rotation);
            GameObject enemy2 = Instantiate(Enemies[Stage-1], this.transform);
            GameObject enemy3 = Instantiate(Enemies[Stage], 
                new Vector3(this.transform.position.x + 20.0f, this.transform.position.y, this.transform.position.z),this.transform.rotation);
        }
    }

}
