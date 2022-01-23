using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] Instances;

    public InterfaceTimer interfaceTimer;
    [HideInInspector] public int CreatibleItemIndex = 2;
    [HideInInspector] public bool bIsChoosen = false;


    private void Update()
    {
        if (this.transform.childCount == 0
            && bIsChoosen)
        {
            InstantiateItems();
            bIsChoosen = false;
        }
    }

    private void InstantiateItems()
    {

        GameObject obj = Instantiate(
                                                      Instances[Random.Range(0, CreatibleItemIndex)],
                                                    this.transform.position,
                                                    new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        obj.transform.parent = this.transform;
    }


}