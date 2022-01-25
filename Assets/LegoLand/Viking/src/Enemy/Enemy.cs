using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  enum EnemyColorType{GREY, BLUE, YELLOW};
public enum EnemyType { Enemy_Missile, Enemy_Arrow, Enemy_Dagger, Enemy_Boss };

public class Enemy : MonoBehaviour
{
    public int DefaultHP { get; private set; }
    public int HP { get; private set; }

    public GameObject HUDInstance;
    private GameObject HUD;

    public EnemyType Type { get; private set; }
    public EnemyColorType ColorType { get; private set; }

    [HideInInspector] public int Index;


    void Start()
    {
        //type
        Type = EnemyType.Enemy_Missile;
        Type = StageManager.Instance.Type;

        //HP
        SetHP();
        HP = DefaultHP;

        //HUD 생성
       HUD =Instantiate(HUDInstance, new Vector2(960,640), Quaternion.identity);
       HUD.GetComponent<EnemyHUD>().Initialized(this.gameObject);
        HUD.name = "EnemyHUD";

        StartCoroutine(SetRandomDirection());
    }

    [Header("Movetest")]
    public int aaindex = 0;

    private void Update()
    {
        if (bSetEnd) Dead();

        time += Time.deltaTime * (speed);


        switch (StageManager.Instance.Stage % 4)
        {
            case 1: { Move(0); } break;
            case 2: { Move(1); } break;
            // case 3: { Move(DirectionIndex); } break; -> 겹치는 문제 발생
            case 3: { Move(1); } break; //-> 위아래로 수정
        }
    }

    private float time = 0;
    public float speed =1.0f;
    private float HorizontalValue = 10.0f;
    private float VerticalValue = 3.0f;
    private int DirectionIndex = 0;

  


    IEnumerator SetRandomDirection()
    {
        while (true)
        {
            DirectionIndex = Random.Range(0, 2);
            Debug.Log(DirectionIndex);
            yield return new WaitForSeconds(4f);
        }
        
    }

    private void Move(int direction)
    {
        switch (direction)
        {
            case 0:
                {
                    transform.position = new Vector3(Mathf.Sin(time) * HorizontalValue, this.transform.position.y, this.transform.position.z);
                }
                break;
            case 1:
                {
                    transform.position = new Vector3( this.transform.position.x, Mathf.Sin(time) * VerticalValue + 9, this.transform.position.z);
                }
                break;
        }

       



    }

    public void initialized(int colorType)
    {
        ColorType = (EnemyColorType)colorType;
    }
    private bool bSetEnd=false;
    void SetHP()
    {
        if (Type == EnemyType.Enemy_Missile)
        {
            if (ColorType == EnemyColorType.GREY) DefaultHP = 100;
            if (ColorType == EnemyColorType.BLUE) DefaultHP = 120;
            if (ColorType == EnemyColorType.YELLOW) DefaultHP = 140;
        }
       else if (Type == EnemyType.Enemy_Arrow)
        {
            if (ColorType == EnemyColorType.GREY) DefaultHP = 120;
            if (ColorType == EnemyColorType.BLUE) DefaultHP = 140;
            if (ColorType == EnemyColorType.YELLOW) DefaultHP = 160;
        }
       else if (Type == EnemyType.Enemy_Dagger)
        {
            if (ColorType == EnemyColorType.GREY) DefaultHP = 140;
            if (ColorType == EnemyColorType.BLUE) DefaultHP = 160;
            if (ColorType == EnemyColorType.YELLOW) DefaultHP = 180;
        }
      else  if (Type == EnemyType.Enemy_Boss)
        {
            if (ColorType == EnemyColorType.GREY) DefaultHP = 150;
            if (ColorType == EnemyColorType.BLUE) DefaultHP = 160;
            if (ColorType == EnemyColorType.YELLOW) DefaultHP = 170;
        }

        bSetEnd = true;
    }


    public void TakeDamage(int value)
    {
        HP = (HP - value) > 0 ? HP - value : 0;
        SoundManager.Instance.PlaySE(SoundList.Sound_monster_hit, transform.position);
        Debug.Log("TakeDamage : " + HP);
    }

    public void Dead()
    {
        if (HP <= 0)
        {
            if (StageManager.Instance.Stage % 4 == 3)
                StageManager.Instance.RemoveEnemies(this.gameObject);
            StageManager.Instance.ChangeStage();
            SoundManager.Instance.PlaySE(SoundList.Sound_monster_death, transform.position);
           //Destroy(HUD);
            Destroy(this.gameObject);
        }
    }
}
