using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpawner : MonoBehaviour
{
    [Header("미사일 관련 변수")]
    public GameObject[] Missiles;
    public float RangeX = 4.0f;
    public float RangeZ = 2.0f;
    public float interval = 5f;

    [Header("화살 관련 변수")]
    public GameObject Arrow;
    public float arrow_range = 3.0f;

    [Header("표창 관련 변수")]
    public GameObject Dagger;
    public float dagger_range = 2.0f;

    private Transform transform_;
    private Quaternion rot;

    private void Awake()
    {
        transform_ = GameObject.Find("AttackSpawnPoint").transform;
        rot = this.transform.rotation;
    }

    
    
    IEnumerator Start()
    {
        float[] direction = new float[4];
        direction[0] = 0;
        direction[1] = 90;
        direction[2] = -90;
        direction[3] = 180.0f;

        while (true)
        {
            switch (StageManager.Instance.Type)
            {
                case EnemyType.Enemy_Missile:
                    {
                        rot= Quaternion.Euler(180.0f, 0, 0);

                        if (StageManager.Instance.ColorType == EnemyColorType.GREY)
                            InstantiateAttack(Missiles[0]);
                       else if (StageManager.Instance.ColorType == EnemyColorType.BLUE)
                            InstantiateAttack(Missiles[1]);
                        else if (StageManager.Instance.ColorType == EnemyColorType.YELLOW)
                            InstantiateAttack(Missiles[2]);
                    }
                    break;
                case EnemyType.Enemy_Arrow:
                    {
                        rot = Quaternion.Euler(0, 0, 0);
                        InstantiateAttack(Arrow);
                    } break;
                case EnemyType.Enemy_Dagger:
                    {

                    } break;
                case EnemyType.Enemy_Boss:
                    {
                    }
                    break;      
            }
            yield return new WaitForSeconds(interval);
        }
    }


    public void InstantiateAttack(GameObject prefab )
    {
        ChangePositionRandom();

        if (StageManager.Instance.Stage % 4 != 3) Instantiate(prefab, this.transform.position, rot);
        else
        {
          
        }
    }

    private void ChangePositionRandom()
    {
        transform.position 
            = new Vector3(Random.Range(-RangeX, RangeX)
                                   , transform.position.y,
                                  Random.Range(-RangeZ, RangeZ));
    }

    /*
    private EnemyType Type = EnemyManager.Instance.Type;

    IEnumerator Start() //일정시간마다 실행
    {
        while (true)
        {
            int index = 0;

            float[] direction = new float[4];
            direction[0] = 0;
            direction[1] = 90;
            direction[2] = -90;
            direction[3] = 180.0f;

          //  if (EnemyManager.Instance.GetIndex() == 0) index = 0;
          //  else if (EnemyManager.Instance.GetIndex() == 1) index = 1;
          //  else if (EnemyManager.Instance.GetIndex() == 2) index = 2;
        //    else   index = Random.Range(0,3);
            Debug.Log("인덱스 결정");

            switch (Type)
            {
                case EnemyType.Enemy_Missile: //Stage 1
                    {
                        InstantiateObjects(Missiles[index], Quaternion.Euler(180.0f, 0, 0));
                    } break;
                case EnemyType.Enemy_Arrow:
                    {
                        InstantiateObjects(Arrow, Quaternion.Euler(0, 0, 0)); } break;
                case EnemyType.Enemy_Dagger: { InstantiateObjects(Dagger, Quaternion.Euler(90.0f, direction[Random.Range(0, 4)], 0)); } break;
                case EnemyType.Enemy_Boss:
                    {
                      //  InstantiateObjects(Missiles[EnemyManager.Instance.GetStage()%4], Quaternion.Euler(180.0f, 0, 0));
                        InstantiateObjects(Arrow, Quaternion.Euler(0, direction[Random.Range(0, 4)], 0));
                        InstantiateObjects(Dagger, Quaternion.Euler(90.0f,0, 0));
                    } break;
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private void Update()
    {
        Type = EnemyManager.Instance.Type;
    }

    private void InstantiateObjects(GameObject obj, Quaternion rot)
    {
       // AttackState state = (AttackState)(EnemyManager.Instance.GetIndex() % 3);
       // Debug.Log(EnemyManager.Instance.GetIndex() % 3);
      //  if (obj == Arrow) { Arrow.GetComponent<Arrow>().state = state; }
       // else if (obj == Dagger) { Dagger.GetComponent<Dagger>().state = state; }

        transform.position = new Vector3(Random.Range(-RangeX, RangeX)
                                                                            , transform.position.y,
                                                                              Random.Range(-RangeZ, RangeZ));
        GameObject obj_ = Instantiate(
                                                    obj,
                                                    transform.position,
                                                    //transform.rotation
                                                    //Quaternion.Euler(180.0f, 0, 0)
                                                    rot  );

       // 0,1,2 ,3/ 3,4,5,6 / 5,6,7,8/ 8,9,10,11 

        
       // if (obj == Missiles) { Missile.GetComponent<Missile>().state = state; }


        obj_.transform.parent = this.gameObject.transform;

    }*/
}
