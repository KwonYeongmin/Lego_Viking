using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   // public AttackState state = AttackState.one;
    public EnemyColorType colortype = EnemyColorType.GREY;
    [Header("���� �ӵ�")]
    [SerializeField]
    private float arrow_fall_speed = 3.0f;

    [Header("���� �ӵ�")]
    private int arrow_damage = 0;
    
    [SerializeField] private int[] Damage;

    [Header("ĳ���� �̵��ӵ� ���� ����")]
    [SerializeField]
    private float arrow_speed_down = 20.0f; //ȭ�� �ǰݿ� ���� ĳ���� �̵��ӵ� ���� ����


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
        SoundManager.Instance.PlaySE(SoundList.Sound_arrow_stick,this.transform.position);
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
        for(int i=0;i<3;i++) particles[i].SetActive(false);
        particles[(int)(colortype)].SetActive(true);

        arrow_damage = Damage[(int)(colortype)];

        projector.material = materials[(int)colortype];
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>()) //�÷��̾ �¾Ҵٸ�
        {
            GiveDamage(collision.gameObject.GetComponent<Collider>());
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == ("deck")) Destroy(this.gameObject,1f);
    }


    private void GiveDamage(Collider collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(arrow_damage); //������ �ֱ�

        float value = collision.gameObject.GetComponent<Movement>().moveSpeed;
        value *= arrow_speed_down;
        collision.gameObject.GetComponent<Player>().DecreaseSpeed(value); //���ǵ� ����

        SoundManager.Instance.PlaySE(SoundList.Sound_arrow_hit, this.transform.position);
    }
}
