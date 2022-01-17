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

    public GameObject[] particles;
    public GameObject[] projectors;

    private Rigidbody rig;
     private GameObject Player;
     private Vector3 targetPosition;

    private GameObject[] deckEdges;
    float[] direction = new float[4];

    private void Awake()
    {
        rig = this.GetComponent<Rigidbody>();
        InitializeState(); // 단계별 피해량 설정

        direction[0] = 0;
        direction[1] = 90;
        direction[2] = -90;
        direction[3] = 180.0f;
        float num = direction[Random.Range(0, 4)];
        this.gameObject.transform.rotation = Quaternion.Euler(90.0f, num, 0);

        Debug.Log(num);
    }


    private void InitializeState()
    {
        switch (state)
        {
            case AttackState.one:
                {
                    dagger_damage = dagger1_damage;
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
                    dagger_damage = dagger2_damage;
                    particles[0].SetActive(false);
                    particles[1].SetActive(true);
                    particles[2].SetActive(false);
                    projectors[0].SetActive(false);
                    projectors[1].SetActive(true);
                    projectors[2].SetActive(false);
                }
                break;
            case AttackState.three: {  dagger_damage = dagger3_damage;
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

    private void Start()
    {

    }

   

    private void Update()
    {
        CheckDeck();
         Move();

        // deck에 떨어지고 나서 이동이  안됨
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>());
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag =="deckEdge")
        { Debug.Log("edge에 닿음"); Destroy(this.gameObject); }
    }

    private void GiveDamage(Collider collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(dagger_damage);
    }

    private bool bIsFallen=false;

    private void Move()
    {

        if (!bIsFallen)
        {
            Vector3 direction = transform.forward;
           float sp= Dagger_fall_speed;
            rig.AddForce(direction * sp);
            Debug.Log("Not bIsFallen");
        }
        else
        {
            Vector3 direction = - transform.up;
            float sp = dagger_speed;
            rig.AddForce(direction * sp);
            transform.Rotate(0.0f,0.0f,0.0f);
            Debug.Log("bIsFallen");
        }

    }

    private void CheckDeck()
    {
        Collider[] collidersEdge = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider collider in collidersEdge)
            if (collider.gameObject.tag == "deck")
                bIsFallen = true;
    }
}
