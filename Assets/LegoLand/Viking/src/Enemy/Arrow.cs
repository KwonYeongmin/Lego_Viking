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
    private float lifeTime = 1.0f;

    public GameObject[] particles;
    public GameObject[] projectors;

    private Rigidbody rig;

    private void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
        InitializeState();
    }

    private void Start()
    {
       Destroy(this.gameObject, 10.0f);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rig.AddForce(- transform.up * arrow_fall_speed);
    }

    private void InitializeState()
    {
        switch (state)
        {
            case AttackState.one:
                {
                    arrow_damage = arrow1_damage;
                    particles[0].SetActive(true);
                    particles[1].SetActive(false);
                    particles[2].SetActive(false);

                    projectors[0].SetActive(true);
                    projectors[1].SetActive(false);
                    projectors[2].SetActive(false);
                }
                break;
            case AttackState.two:
                {
                    arrow_damage = arrow2_damage; 
                     particles[0].SetActive(false);
                    particles[1].SetActive(true);
                    particles[2].SetActive(false);
                    projectors[0].SetActive(false);
                    projectors[1].SetActive(true);
                    projectors[2].SetActive(false);
                }
                break;
            case AttackState.three:
                {
                    arrow_damage = arrow3_damage; //
                    particles[0].SetActive(false);
                    particles[1].SetActive(false);
                    particles[2].SetActive(true);

                    projectors[0].SetActive(false);
                    projectors[1].SetActive(false);
                    projectors[2].SetActive(true);
                }
                break;
        }
    }


    private void OnDrawGizmos()
    {
        // Debug.Log("OnDrawGizmos");

        Gizmos.color = Color.red;
      //  Gizmos.DrawWireSphere(transform.position, Missile_damage_edge);

        Gizmos.color = Color.yellow;
     //   Gizmos.DrawWireSphere(transform.position, Missile_damage_center);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("deck")) Destroy(this.gameObject,1f);

        if (collision.gameObject.GetComponent<Player>()) //플레이어가 맞았다면
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>());
            Destroy(this.gameObject);
        }
    }


    private void GiveDamage(Collider collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(arrow_damage); //데미지 주기

        float value = collision.gameObject.GetComponent<Movement>().moveSpeed;
        value *= arrow_speed_down;
        collision.gameObject.GetComponent<Movement>().DecreaseSpeed(value); //스피드 감소
    }
}
