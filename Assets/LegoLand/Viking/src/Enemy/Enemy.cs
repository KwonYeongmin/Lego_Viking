using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int DefaultHP = 100;
    private int HP ;

    private Image HPImg;

    void Start()
    {
       HP = DefaultHP;
    }

    void Update()
    {

        if (GameObject.FindWithTag("Enemy").gameObject.GetComponent<Image>())
        {
            HPImg= GameObject.FindWithTag("Enemy").gameObject.GetComponent<Image>();
            HPImg.fillAmount = (float)HP / (float)DefaultHP;
        }

        Dead();

    }

    public void TakeDamage(int value)
    {
        HP = (HP - value) > 0 ? HP - value : 0;
        Debug.Log("TakeDamage : " + HP);
       // if (HP <= 0)
        //    Destroy(gameObject);
    }

    public void Dead()
    {
        if (HP <= 0)
        {
            EnemyManager.Instance.ChangeStage();
            Destroy(this.gameObject);
        }
    }
}
