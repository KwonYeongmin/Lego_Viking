using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public AttackState state = AttackState.one;

    [Header("낙하 속도")]
    [SerializeField]
    private float Dagger_fall_speed = 2.0f; 

    [Header("낙하 후 이동 거리")]
    [SerializeField]
    private float Dagger_range = 2.0f; 

    [Header("낙하 후 속도")]
    [SerializeField]
    private float dagger_speed = 2.0f;

    [Header("피해량")]
    private int dagger_damage = 0;
    [SerializeField]
    private int dagger1_damage = 1;
    [SerializeField]
    private int dagger2_damage = 2;
    [SerializeField]
    private int dagger3_damage = 3;

    [Header("lifetime")]
    [SerializeField]
    private float lifeTime = 10.0f;


    private Rigidbody rig;
     private GameObject Player;
     private Vector3 targetPosition;

    private void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
        InitializeState(); // 단계별 피해량 설정
    }


    private void InitializeState()
    {
        switch (state)
        {
            case AttackState.one:
                {
                    dagger_damage = dagger1_damage;
                }
                break;
            case AttackState.two:
                {
                    dagger_damage = dagger2_damage;
                }
                break;
            case AttackState.three:
                {
                    dagger_damage = dagger3_damage;
                }
                break;
        }
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        targetPosition = Player.transform.position;
        Destroy(this.gameObject, lifeTime);
    }

    private void Update()
    {
        //targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);

       // if (bIsFallen)
        {
            targetPosition = Player.transform.position;
            transform.LookAt(targetPosition);
        }
        

        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GiveDamage(collision);
            Destroy(this.gameObject);
        }
    }

    private void GiveDamage(Collision collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(dagger_damage);
    }

    private bool bIsFallen=false;

    private void Move()
    {
        Vector3 direction = transform.forward;// -transform.right;
        float speed = Dagger_fall_speed;

        rig.AddForce( direction * speed);

        Collider[] collidersEdge = Physics.OverlapSphere(transform.position, 1f);

        foreach (Collider collider in collidersEdge)
        {
            if (collider.gameObject.tag == "deck")
            {
                Debug.Log("deck에 닿음 ");
                //bIsFallen = true;
                direction = transform.forward;
                speed = dagger_speed;
            }
            else
            {
                direction = - transform.right;
                speed = Dagger_fall_speed;
            }
        }


        
    }
}
