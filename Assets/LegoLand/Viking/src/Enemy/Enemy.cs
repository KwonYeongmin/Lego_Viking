using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 100;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(int value)
    {
        HP = (HP - value) > 0 ? HP - value : 0;
        if (HP <= 0)
            Destroy(gameObject);
    }
}
