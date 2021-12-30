using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AttackState state = AttackState.one;

    [Header("낙하 속도")]
    [SerializeField]
    private float arrow_fall_speed = 3.0f;

    [Header("낙하 속도")]
    private int arrow_damage = 0;
    [SerializeField]
    private int arrow1_damage = 1;
    private int arrow2_damage = 2;
    private int arrow3_damage = 3;

    [Header("캐릭터 이동속도 감소 비율")]
    [SerializeField]
    private float arrow_speed_down = 20.0f; //화살 피격에 의한 캐릭터 이동속도 감소 비율


    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 10.0f;


    Rigidbody rig;

    private void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
        InitializeState();
    }

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rig.AddForce(transform.forward * arrow_fall_speed);
    }

    private void InitializeState()
    {
        switch (state)
        {
            case AttackState.one:
                {
                    arrow_damage = arrow1_damage;
                }
                break;
            case AttackState.two:
                {
                    arrow_damage = arrow2_damage;
                }
                break;
            case AttackState.three:
                {
                    arrow_damage = arrow3_damage;
                }
                break;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("deck")) Destroy(this.gameObject);

        if (collision.gameObject.GetComponent<Player>()) //플레이어가 맞았다면
        {
            GiveDamage(collision);
            Destroy(this.gameObject);
        }
    }


    private void GiveDamage(Collision collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(arrow_damage); //데미지 주기

        float value = collision.gameObject.GetComponent<Movement>().moveSpeed;
        value *= arrow_speed_down;
        collision.gameObject.GetComponent<Movement>().DecreaseSpeed(value); //스피드 감소
    }
}
