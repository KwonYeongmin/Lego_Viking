using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpawner : MonoBehaviour
{
    [Header("화살 관련 변수")]
    public GameObject Arrow;
    public float arrow_range = 3.0f;


    [Header("표창 관련 변수")]
    public GameObject Dagger;
    public float dagger_range = 2.0f;
    
    [Header("미사일 관련 변수")]
    public GameObject Missile;
   // public Transform MissileSpawner;
 

    public float Range = 5.0f;
    //public float[] Interval;
    public float interval = 5f;
    private int spaceSaver = 0;

    public int type = 0;

    IEnumerator Start() //일정시간마다 실행
    {
        Debug.Log("start");
        while (true)
        {
            //switch (spaceSaver)
            switch (type)
            {
                case 0: { InstantiateObjects(Missile, Quaternion.Euler(180.0f, 0, 0)); } break;
                case 1:
                    {
                        float[] direction = new float[4];
                        direction[0] = 0;
                        direction[1] = 90;
                        direction[2] = -90;
                        direction[3] = 180;
                        InstantiateObjects(Arrow, Quaternion.Euler(0, direction[Random.Range(0, 4)], 0)); } break;
                case 2: { InstantiateObjects(Dagger, Quaternion.Euler(90.0f,0, 0)); } break;
            }
            Debug.Log("생성");
            yield return new WaitForSeconds(interval);
           
        }
    }

    private void Update()
    {
        spaceSaver %= 3;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceSaver++;
            Debug.Log("space 누름");
        }
            
    }

    private void InstantiateObjects(GameObject obj, Quaternion rot)
    {

        transform.position = new Vector3(Random.Range(-Range, Range)
                                                                            , transform.position.y,
                                                                              transform.position.z);
        GameObject obj_ = Instantiate(
                                                    obj,
                                                    transform.position,
                                                    //transform.rotation
                                                    //Quaternion.Euler(180.0f, 0, 0)
                                                    rot
                                                    );
        obj_.transform.parent = this.gameObject.transform;

    }
}
