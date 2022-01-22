using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   // public AttackState state = AttackState.one;
    public EnemyColorType colortype = EnemyColorType.GREY;
    [Header("낙하 속도")]
    [SerializeField]
    private float arrow_fall_speed = 3.0f;

    [Header("낙하 속도")]
    private int arrow_damage = 0;
    
    [SerializeField] private int arrow1_damage = 1;
    [SerializeField] private int arrow2_damage = 2;
    [SerializeField] private int arrow3_damage = 3;

    [Header("캐릭터 이동속도 감소 비율")]
    [SerializeField]
    private float arrow_speed_down = 20.0f; //화살 피격에 의한 캐릭터 이동속도 감소 비율


    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 1.0f;

    [Header("Particles")]
    public GameObject[] particles;

    [Header("Projector")]
    public Projector projector;
    public Material[] materials;

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

    public void InitializeState()
    {
        Debug.Log("InitializeState");
        switch (colortype)
        {
            case EnemyColorType.GREY:
                {
                    arrow_damage = arrow1_damage;
                    projector.material = materials[0];

                    particles[0].SetActive(true);
                    particles[1].SetActive(false);
                    particles[2].SetActive(false);
                    
                }
                break;
            case EnemyColorType.BLUE:
                {
                    arrow_damage = arrow2_damage; 
                    particles[0].SetActive(false);
                    particles[1].SetActive(true);
                    particles[2].SetActive(false);
                    projector.material = materials[1];
                }
                break;
            case EnemyColorType.YELLOW:
                {
                    arrow_damage = arrow3_damage; //
                    particles[0].SetActive(false);
                    particles[1].SetActive(false);
                    particles[2].SetActive(true);
                    projector.material = materials[2];
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
        if (collision.gameObject.GetComponent<Player>()) //플레이어가 맞았다면
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>());
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == ("deck")) Destroy(this.gameObject,1f);
    }


    private void GiveDamage(Collider collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(arrow_damage); //데미지 주기

        float value = collision.gameObject.GetComponent<Movement>().moveSpeed;
        value *= arrow_speed_down;
        collision.gameObject.GetComponent<Player>().DecreaseSpeed(value); //스피드 감소
    }
}
