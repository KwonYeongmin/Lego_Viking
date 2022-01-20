using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // ȸ��, �Ѿ�, ���ǵ��, ��ź, ����
    public enum ItemType { Heal = 0, Bullet = 1, Speedup = 2, Grenade = 3, Invincible = 4 };

    [Header("������ Ÿ��")]
    public ItemType itemType = ItemType.Heal;

    [Header("ȸ�� ������ ����")]
    [Tooltip("ȸ�� ���� ���")]
    public float heal_Recovery = 20; //���� ü���� 20%��ŭ ȸ��
    
    [Header("źâ ������ ����")]
    [Tooltip("źâ ���� ����")]
   public int recovery_bullet = 10;

    [Header("�̵��ӵ� ���� ������ ����")]
    [Tooltip("�̵��ӵ� ���� ���")]
    public int buff_speedup =20 ;
    [Tooltip("������ ��� �ð�")]
    public float duration_speedup =5.0f ;
    

    [Header("���� ������ ����")]
    [Tooltip("������ ��� �ð�")]
    public float duration_invincible = 5.0f;

    [Header("�ı��ð�")]
    public float Lifetime = 5.0f;

    private void Start()
    {
         Destroy(this.gameObject, Lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            switch (itemType)
            {
                case ItemType.Heal: { UseHealItem(other); } break;
                case ItemType.Bullet: { UseBulletItem(other); } break;
                case ItemType.Speedup: { UseSpeedupItem(other); } break;
                case ItemType.Grenade: { UseBombItem(other); } break;
                case ItemType.Invincible: { UseInvincibleItem(other); } break;
            }

            Destroy(this.gameObject);
        }
    }

  


    private void UseHealItem(Collider other)
    {
        Debug.Log("ȸ�� ������ ���");
        float hp = other.gameObject.GetComponent<Player>().HP;
        hp *= (heal_Recovery*0.01f);
        Debug.Log("ȸ���� : " +  hp);
        other.gameObject.GetComponent<Player>().AddHP(Mathf.RoundToInt(hp));
    }


    private void UseBulletItem(Collider other)
    {
        Debug.Log("�Ѿ� ������ ���");
        other.gameObject.GetComponent<Player>().AddAmmo(recovery_bullet);
    }


    private void UseSpeedupItem(Collider other)
    {
        Debug.Log("���ǵ�� ������ ���");
        float speed = other.gameObject.GetComponent<Movement>().defaultMoveSpeed;
        speed += speed * (buff_speedup*0.01f);
        other.gameObject.GetComponent<Player>().AddSpeedUp(speed, duration_speedup);
    }


    private void UseBombItem(Collider other)
    {
        Debug.Log("����ź ������ ���");
        other.gameObject.GetComponent<Player>().AddGrenade();
    }

    private void UseInvincibleItem(Collider other)
    {
        Debug.Log("���� �����ۻ��");
        other.gameObject.GetComponent<Player>().AddInvincible(duration_invincible);
    }

}

public class Timer
{
    private float timer;
    private bool bIsTimerStop = false;

    public void StartTimer() { bIsTimerStop = false; }
    public void StopTimer() { bIsTimerStop = true; }
    public void ResetTimer() { timer = 0; }
    public float GetTimer() { return timer; }
    public bool GetTimerStopState() { return bIsTimerStop; } // true�� stop, false�� stopX
    public void UpdateTimer()
    {
        if (bIsTimerStop) return;
        timer += Time.deltaTime;
    }
}



/*
   Timer countdownTimer = new Timer();
   [HideInInspector]  public TMP_Text TimeTEXT;

   private void Start()
   {
       StartTimer();
   }

   void StartTimer() 
   {
       countdownTimer.ResetTimer();
       countdownTimer.StartTimer();
   }
   private void FixedUpdate()
   {
       countdownTimer.UpdateTimer();
   }

   void Update()
   {

       int countdown = 3-(int)(countdownTimer.GetTimer());
       TimeTEXT.text = countdown.ToString();


       if (countdownTimer.GetTimer() >= 3)
       {
           this.gameObject.SetActive(false);
           countdownTimer.ResetTimer();
       }
   }
   */
