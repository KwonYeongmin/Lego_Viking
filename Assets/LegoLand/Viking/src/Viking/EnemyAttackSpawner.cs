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
    private float interval = 5f;
    private int spaceSaver = 0;

    IEnumerator Start() //일정시간마다 실행
    {
        while (true)
        {
            // InstantiateObjects(Missile);
            switch (spaceSaver)
            {
                case 0: { InstantiateObjects(Missile, Quaternion.Euler(180.0f, 0, 0)); } break;
                case 1: { InstantiateObjects(Arrow, Quaternion.Euler(0, 0, 0)); } break;
                case 2: { InstantiateObjects(Dagger, Quaternion.Euler(0, 0, 90.0f)); } break;
            }
            
            yield return new WaitForSeconds(interval); 
        }
    }

    private void Update()
    {
        spaceSaver %= 3;
        if (Input.GetKeyDown(KeyCode.Space))
            spaceSaver++;
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
