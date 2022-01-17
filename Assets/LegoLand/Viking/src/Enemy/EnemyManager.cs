using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Enemy_Missile, Enemy_Arrow, Enemy_Dagger, Enemy_Boss };

public class EnemyManager : MonoBehaviour
{
    #region EnemyVariables

    public EnemyType Type = EnemyType.Enemy_Missile;

    public List<GameObject> enemies= new List<GameObject>(); 

    private int Stage = 0; 
    private int InStageNum = 0; //0123 4567 891011 12131415
    public int GetStage() { return Stage; }
    public int GetInStageNum() { return InStageNum; }

    private EnemySpawner enemySpawner;
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

        enemySpawner = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<EnemySpawner>();
    }
    #endregion

    /*

 EnemyManager - > 적 종류, 공격 종류 지정
enemySpawner 적 생성
AttackSpawner 공격 생성
*/

    /*
    void Start()
    {
        InstantiateEnemy();
    }
    private void Update()
    {
        // Debug.Log(enemies_stage_final.Count);
        Debug.Log("Stage : " + Stage);
    }

    public void ChangeStage_final(GameObject obj)
    {
        enemies_stage_final.Remove(obj);


        if (enemies_stage_final.Count == 0)
        {
            ChangeStage();
        }
       
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

    public int GetIndex()
    {
        int index = Stage;

        if (Type == EnemyType.Enemy_Missile) index = Stage;
        else if (Type == EnemyType.Enemy_Arrow) index = Stage - 1;
        else if (Type == EnemyType.Enemy_Dagger) index = Stage - 2; // 3
        else if (Type == EnemyType.Enemy_Boss) index = Stage - 3;

        return index;
    }

    void OnChangeEnemyType()
    {
        if (Stage < 4) Type = EnemyType.Enemy_Missile;
        else if (Stage >= 4 && Stage < 8) Type = EnemyType.Enemy_Arrow;
        else if (Stage >= 8 && Stage < 12) Type = EnemyType.Enemy_Dagger;
        else if (Stage >= 12 && Stage < 16) Type = EnemyType.Enemy_Boss;
    }

    GameObject[] enemies= new GameObject[3];

    void InstantiateEnemy()
    {
        if (Stage % 4 != 3) // 3,7,11,15 // 0,1,2,3 // 4,5,6,7
        { 
            int index = Stage;

            if (Type == EnemyType.Enemy_Missile) index = Stage;
           else  if (Type == EnemyType.Enemy_Arrow) index = Stage-1;
           else if (Type == EnemyType.Enemy_Dagger) index = Stage-2; // 3
           else if (Type == EnemyType.Enemy_Boss) index = Stage-3;

            Debug.Log(index);
          
            enemies[0] = Instantiate(Enemies[GetIndex()], this.transform);
            enemies[0].transform.parent = this.transform;
            /*
                  if (Type == EnemyType.Enemy_Arrow)
                  {

                  }
                  if (Type == EnemyType.Enemy_Arrow)
                      {
                      enemies[0] = Instantiate(Enemies[Stage], this.transform); // 0, 1, 2
                      enemies[0].transform.parent = this.transform;
                  }
                  else if (Type == EnemyType.Enemy_Dagger)
                  {
                      enemies[0] = Instantiate(Enemies[Stage-1], this.transform); // 4, 5, 6 // 3, 4, 5
                      enemies[0].transform.parent = this.transform;
                  }
                  else if (Type == EnemyType.Enemy_Boss)
                  {
                      enemies[0] = Instantiate(Enemies[Stage-2], this.transform);
                      enemies[0].transform.parent = this.transform;
                  }       */

    /*
        }
        else // 3,7,11,15
        {

            enemies[0] = Instantiate(Enemies[GetIndex()- 3], // 2 4
              new Vector3(this.transform.position.x - 10.0f, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            enemies[0].transform.parent = this.transform;

            enemies[1] = Instantiate(Enemies[GetIndex()-2], this.transform); // 1 5
            enemies[1].transform.parent = this.transform;


            enemies[2] = Instantiate(Enemies[GetIndex()-1],  // 0 6
                new Vector3(this.transform.position.x + 10.0f, this.transform.position.y, this.transform.position.z),this.transform.rotation);
            enemies[2].transform.parent = this.transform;

            enemies_stage_final.Add(enemies[0]);
            enemies_stage_final.Add(enemies[1]);
            enemies_stage_final.Add(enemies[2]);
        }
    }
    
*/


    public void ChangeStage()
    {
        Stage++;
        if (Stage % 4 == 0) InStageNum = 0;

        SettingStage();
    } 

    void SettingStage()
    {
        Type = (EnemyType)(Stage / 4);         
    }


    public void InstantiateEnemies()
    {
        enemySpawner.InstantiateEnemy();
    }

    // 스테이지 관리자 
    // MakeEnemy()
    // 생성 
    // -> 적 공격, 적(적 캐릭터, HUD)
}
