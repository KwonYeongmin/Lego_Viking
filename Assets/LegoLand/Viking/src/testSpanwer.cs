using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSpanwer : MonoBehaviour
{
    public GameObject dagger;
    private float RangeX = 4.0f;
    private float RangeZ = 2.0f;
    float[] direction = new float[4];

    private void Awake()
    {
        direction[0] = 0;
        direction[1] = 90;
        direction[2] = -90;
        direction[3] = 180.0f;
    }

    IEnumerator Start()
    {
        while (true)
        {
            transform.position = new Vector3(Random.Range(-RangeX, RangeX)
                                                                          , transform.position.y,
                                                                            Random.Range(-RangeZ, RangeZ));
            Instantiate(dagger,transform.position, Quaternion.Euler(90.0f, direction[Random.Range(0, 4)], 0));
            yield return new WaitForSeconds(3f);

        }
    }

    
    void Update()
    {
    }
}
