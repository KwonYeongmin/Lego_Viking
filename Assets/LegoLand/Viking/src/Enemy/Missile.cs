using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missile : MonoBehaviour
{
    EnemyColorType colorType = EnemyColorType.GREY;
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
    private float Missile_range_edge = 3.0f; //�̻��� �ܰ� ���ع���
    private float Missile_range_center = 2.0f; //�̻��� �߾� ���ع���

    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 10.0f;
    
    private Rigidbody rigid;


    public GameObject FX;


    private void Awake()
    {
        InitializeState(); //�̻��� �ܰ躰 ���ط� ����
    }


    private void InitializeState()
    {
        switch (colorType)
        {

            case EnemyColorType.GREY:
                {
                    Missile_damage_center = Missile1_damage_center;
                    Missile_damage_edge = Missile1_damage_edge;
                } break;
            case EnemyColorType.BLUE:
                {
                    Missile_damage_center = Missile2_damage_center;
                    Missile_damage_edge = Missile2_damage_edge;
                } break;
            case EnemyColorType.YELLOW:
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
        SoundManager.Instance.PlaySE(SoundList.Sound_missile_explosion, this.transform.position);
    }

    private void Update()
    {
        Move();
        JudgeEdgePlayer(Missile_damage_edge);
        JudgeEdgePlayer(Missile_damage_center);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "deck") //����ŷ ���ǿ� �ε����� �ı�
        {
            Instantiate(FX, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + 5f, collision.gameObject.transform.position.z)
                                              , collision.gameObject.transform.rotation);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.GetComponent<Player>()) // �÷��̾�� �ε�����
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>(), Missile_damage_center); //�̻��� �߾� ���� ����
            Destroy(this.gameObject);
        }
    }
    
    private void GiveDamage(Collider collision,int damage)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(damage);
        Instantiate(FX, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y +5f , collision.gameObject.transform.position.z)
                                                ,collision.gameObject.transform.rotation);
        SoundManager.Instance.PlaySE(SoundList.Sound_missile_hit,this.transform.position);
    }


    private void JudgeEdgePlayer(int damage) 
    {
        Collider[] collidersEdge = Physics.OverlapSphere(transform.position, damage);

        foreach (Collider collider in collidersEdge)
        {
            if (collider.gameObject.GetComponent<Player>())
            {
                GiveDamage(collider.gameObject.GetComponent<Collider>(), damage);
               // if (damage == Missile_damage_center) Debug.Log("�߽� ������");
                // if (damage == Missile_damage_edge) Debug.Log("�ܰ� ������");
                Destroy(this.gameObject);
            }
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Missile_damage_edge);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Missile_damage_center);
    }


    private void Move()
    {
        rigid.AddForce(transform.up* Missile_fall_speed);
    }
}
