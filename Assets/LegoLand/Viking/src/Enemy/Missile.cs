using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missile : MonoBehaviour
{
    EnemyColorType colorType = EnemyColorType.GREY;
  [Header("�߾� ���ط�")]
    private int Missile_damage_center = 0;
    [SerializeField]
    private int[] Damage_center;

    [Header("�ܰ� ���ط�")]
    private int Missile_damage_edge = 0;
    [SerializeField]
    private int[] Damage_Edge;

    [Header("���� �ӵ�")]
    [SerializeField]
    private float Missile_fall_speed = 1.0f ; //�̻��� ���ϼӵ�

    [Header("���� ����")]
    [SerializeField]
    private float Missile_range_edge = 3.0f; //�̻��� �ܰ� ���ع���
    [SerializeField]
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
        Missile_damage_center = Damage_center[(int)(colorType)];
        Missile_damage_edge = Damage_Edge[(int)(colorType)];
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Deck") ) //����ŷ ���ǿ� �ε����� �ı�
        {
            GameObject obj= Instantiate(FX, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 0.5f, this.gameObject.transform.position.z)
                                                , this.gameObject.transform.rotation);
            Destroy(obj.gameObject, 2f);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.GetComponent<Player>()) // �÷��̾�� �ε�����
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>(), Missile_damage_center-Missile_damage_edge); //�̻��� �߾� ���� ����
            Debug.Log("�߽�");

            Destroy(this.gameObject);
        }
    }

    private bool bDamage =false;

    private void GiveDamage(Collider collision,int damge)
    {
      if(!bDamage)  collision.gameObject.GetComponent<Player>().TakeDamage(damge);
        bDamage = true;
        Instantiate(FX, new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y +5f , collision.gameObject.transform.position.z)
                                                ,collision.gameObject.transform.rotation);
        SoundManager.Instance.PlaySE(SoundList.Sound_missile_hit,this.transform.position);
    }


    private void JudgeEdgePlayer(int range) 
    {
        Collider[] collidersEdge = Physics.OverlapSphere(transform.position, Missile_range_edge);

        foreach (Collider collider in collidersEdge)
        {
            if (collider.gameObject.GetComponent<Player>())
            {
                GiveDamage(collider.gameObject.GetComponent<Collider>(), Missile_damage_edge);
                Debug.Log("�ܰ�");
                Destroy(this.gameObject);
            }
        } 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Missile_range_center);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Missile_range_edge);
    }


    private void Move()
    {
        rigid.AddForce(transform.up* Missile_fall_speed);
    }
}
