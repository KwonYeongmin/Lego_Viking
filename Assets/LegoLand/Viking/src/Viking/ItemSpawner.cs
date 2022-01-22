using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
     public GameObject[] Instances;
    
    public GameObject SpawnPoint;
    private Transform[] SpawnPoints;
    public int SpawnPointNum =10;
    public float Interval = 5.0f;
    public int InstanceCount = 10;
    public float Range = 5.0f;
    public InterfaceTimer interfaceTimer;

    //
    public int CreatibleItemIndex = 2;

    private void Awake()
    {
        SpawnPoints = new Transform[SpawnPointNum];

        for (int i = 0; i < SpawnPointNum; i++)
            SpawnPoints[i] = SpawnPoint.transform.GetChild(i).gameObject.GetComponent<Transform>();

    }

    IEnumerator Start()
    {
        if (!interfaceTimer.isPlay)
            yield return new WaitForSeconds(9.0f);

        for (int i = 0; i < InstanceCount; i++)
        {
             InstantiateItems();
            yield return new WaitForSeconds(Interval);
        }
    }

     private void InstantiateItems()
    {
        int posIndex = Random.Range(0, SpawnPointNum);
        GameObject obj = Instantiate(
                                                      Instances[Random.Range(0, CreatibleItemIndex)],
                                                     SpawnPoints[posIndex].position,
                                                     this.transform.rotation);
        obj.transform.parent = SpawnPoints[posIndex];
    }


}
