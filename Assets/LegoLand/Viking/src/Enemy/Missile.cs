using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackState { one, two, three,Max };


public class Missile : MonoBehaviour
{
    public AttackState state = AttackState.one;

  [Header("�߾� ���ط�")]
    private int Missile_damage_center = 0;
    [SerializeField]
    private int Missile1_damage_center = 2;
    private int Missile2_damage_center = 3;
    private int Missile3_damage_center = 4;

    [Header("�ܰ� ���ط�")]
    private int Missile_damage_edge = 0;
    [SerializeField]
    private int Missile1_damage_edge = 1;
    private int Missile2_damage_edge = 2;
    private int Missile3_damage_edge = 3;

    [Header("���� �ӵ�")]
    [SerializeField]
    private float Missile_fall_speed = 1.0f ; //�̻��� ���ϼӵ�

    [Header("���� ����")]
    [SerializeField]
    private float Missile_range_edge = 1.0f; //�̻��� �ܰ� ���ع���
    private float Missile_range_center = 2.0f; //�̻��� �߾� ���ع���

    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 10.0f;


    private Rigidbody rigid;


    private void Awake()
    {
        InitializeState(); //�̻��� �ܰ躰 ���ط� ����
    }



    private void InitializeState()
    {
        switch (state)
        {
            case AttackState.one:
                {
                    Missile_damage_center = Missile1_damage_center;
                    Missile_damage_edge = Missile1_damage_edge;
                } break;
            case AttackState.two:
                {
                    Missile_damage_center = Missile2_damage_center;
                    Missile_damage_edge = Missile2_damage_edge;
                } break;
            case AttackState.three:
                {
                    Missile_damage_center = Missile3_damage_center;
                    Missile_damage_edge = Missile3_damage_edge;
                } break;
        }
    }



    private void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
    }


    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag== "deck") //����ŷ ���ǿ� �ε����� �ı�
            Destroy(this.gameObject);

        if (collision.gameObject.GetComponent<Player>()) // �÷��̾�� �ε�����
        {
            GiveDamage(collision, Missile_damage_center); //�̻��� �߾� ���� ����
            Destroy(this.gameObject);
        }
    }
    
    private void GiveDamage(Collision collision,int damage)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(damage);
    }

    private void JudgeEdgePlayer() // �̻��� �ܰ� ���� ����
    {
        Collider[] collidersEdge = Physics.OverlapSphere(transform.position, Missile_damage_edge);
        foreach (Collider collider in collidersEdge)
        {
            if (collider.gameObject.GetComponent<Player>())
            {
                GiveDamage(collider.gameObject.GetComponent<Collision>(), Missile_damage_edge);
                Destroy(this.gameObject);
            }
        } 
    }

    private void Move()
    {
        rigid.AddForce(transform.up* Missile_fall_speed);
    }
}
