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

    private void Awake()
    {
        SpawnPoints = new Transform[SpawnPointNum];

        for (int i = 0; i < SpawnPointNum; i++)
            SpawnPoints[i] = SpawnPoint.transform.GetChild(i).gameObject.GetComponent<Transform>();
    }

    IEnumerator Start()
    {
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
                                                      Instances[Random.Range(0, Instances.Length)],
                                                     SpawnPoints[posIndex].position,
                                                     this.transform.rotation);
        obj.transform.parent = SpawnPoints[posIndex];
        /*
             int nRan = 0;
while(true)
{
    nRan = Random.Range(1, 6);
    if(nRan == 2)
        continue;
    else
        break;
}
         */
    }


}
