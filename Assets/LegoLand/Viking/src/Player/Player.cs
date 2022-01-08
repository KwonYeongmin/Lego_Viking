using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables

    private Vector3 direction;
    private Movement movement;
    private PlayerAnimator animator;

    #region Input variables
    // #. Keyboard
    private float hAxis;
    private float vAxis;

    // #. Joypad(KeyPad)
    //[HideInInspector]
    public bool[] keyControl = new bool[9];
    //[HideInInspector]
    public bool isControl;
    [HideInInspector]
    public bool isButtonRoll;
    [HideInInspector]
    public bool isButtonFire;
    [HideInInspector]
    public bool isButtonReload;
    [HideInInspector]
    public bool isButtonGrenade;
    #endregion

    #region Weapon variables
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    float fireDelay;
    bool isFireReady;
    public int defaultAmmo; //�߰�
    [HideInInspector]public int ammo;//����

    [Header("Grenade")]
    public GameObject grenadeObj;
    public Transform grenadePos;
    private bool bIsGrenadesEnable = true; //�߰� : ����ź ������ ��������
    private int maxGrenades = 3;
    public int hasGrenades;
    public float throwPower;
    public float throwHeight;
    #endregion

    #region HP variables
    [Header("HP")]
    public int DefaultHP;
    [HideInInspector] public int HP;

    public float invincible_Duration;
    public bool isInvincible = false;
    #endregion

    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<PlayerAnimator>();
        weapon = GetComponentInChildren<Weapon>();

        HP = DefaultHP; // �߰�
        ammo = defaultAmmo; // �߰�
    }

    // Update is called once per frame
    private void Update()
    {
        GetInput(); // Update Input

        Move();
        Roll();
        Fire();
        Grenade();

        if(isInvincible)
            UpdateTimer(); // �߰� : UpdateInput
    }

    #region Input

    private void GetInput()
    {
        // #. Keypad Control
        if (keyControl[0]) { hAxis = -1; vAxis = 1; }
        if (keyControl[1]) { hAxis = 0; vAxis = 1; }
        if (keyControl[2]) { hAxis = 1; vAxis = 1; }
        if (keyControl[3]) { hAxis = -1; vAxis = 0; }
        if (keyControl[4]) { hAxis = 0; vAxis = 0; }
        if (keyControl[5]) { hAxis = 1; vAxis = 0; }
        if (keyControl[6]) { hAxis = -1; vAxis = -1; }
        if (keyControl[7]) { hAxis = 0; vAxis = -1; }
        if (keyControl[8]) { hAxis = 1; vAxis = -1; }
        if (!isControl) { hAxis = 0; vAxis = 0; }

        // #. KeyBoard Control
        // hAxis = Input.GetAxisRaw("Horizontal"); // ��,�� ������
        // vAxis = Input.GetAxisRaw("Vertical"); // ��, �Ʒ� ������

        isButtonRoll = Input.GetKeyDown(KeyCode.LeftShift);
        isButtonFire = Input.GetButton("Fire2"); //("Fire1");
        isButtonReload = Input.GetKeyDown(KeyCode.R);
        isButtonGrenade = Input.GetKeyDown(KeyCode.G);
    }

    #endregion

    #region Mobile KeyPad

    public void KeyPad(int type)
    {
        for (int i = 0; i < 9; i++)
        {
            keyControl[i] = i == type;
        }
    }

    public void KeyDown()
    {
        isControl = true;
    }

    public void KeyUp()
    {
        isControl = false;
    }

    public void ButtonRollDown()
    {
        if (!movement.isRoll)
        {
            isButtonRoll = true;
            Roll();                     //  �߰�
        }

    }

    public void ButtonFireDown()
    {
        isButtonFire = true;
        Fire();                          //  �߰�
    }

    public void ButtonFireUp()
    {
        isButtonFire = false;
    }

    public void ButtonGrenadeDown()
    {
        isButtonGrenade = true;
        Grenade();                       //  �߰�
    }

    #endregion

    #region Movement & Roll

    private void Move()
    {
        direction = new Vector3(hAxis, 0, vAxis);

        movement.MoveTo(direction);

        animator.OnMovement(Mathf.Clamp01(Mathf.Abs(hAxis) + Mathf.Abs(vAxis)));
    }

    private void Roll()
    {
        if (isButtonRoll && !movement.isRoll && !isButtonReload)
        {
            Debug.Log("Roll");
            movement.isRoll = true;
            movement.Roll(direction);
            animator.OnRoll();
        }
    }

    #endregion

    #region Action

    private void Fire()
    {
        if (weapon == null) return;

        fireDelay += Time.deltaTime;
        isFireReady = weapon.rate < fireDelay;

        if (isButtonFire && isFireReady && !isButtonRoll)
        {
            weapon.Use();
            animator.OnFire();
            fireDelay = 0;
            ammo--; //�߰�
        }
    }

    private void Grenade()
    {
        if (hasGrenades == 0 || !bIsGrenadesEnable) return; //�߰� : bIsGrenadesEnable �߰�

        if (isButtonGrenade)
        {
            Debug.Log("Grenade");
            GameObject instantGrenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
            Rigidbody rigidGrenade = instantGrenade.GetComponent<Rigidbody>();
            Vector3 throwVec = transform.forward * throwPower;
            throwVec.y = throwHeight;

            rigidGrenade.AddForce(throwVec, ForceMode.Impulse);
            rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

            hasGrenades--;
            if (hasGrenades == 0)
                bIsGrenadesEnable = false;
        }

    }

    #endregion

    #region Damage
    public void TakeDamage(int value)
    {
        if (isInvincible) return;

        HP = (HP - value) > 0 ? HP - value : 0; // �߰�
        Debug.Log("HP: " +HP);
        Debug.Log("value: " +  value);
    }
    #endregion


    // ====== �߰�

    // HP ȸ��
    public void AddHP(int value)
    {
        HP = HP + value < DefaultHP ? HP + value : DefaultHP;
        Debug.Log("Add HP : " + HP);
    }

    // źâ ȸ��
    public void AddAmmo(int value)
    {
        ammo = ammo + value < defaultAmmo ?   ammo + value : defaultAmmo;
        weapon.currentAmmo = ammo;
        Debug.Log("Add ammo : "+ ammo);
    }

    public void AddGrenade()
    {
        if(hasGrenades < maxGrenades)
            hasGrenades++;
        bIsGrenadesEnable = true;
    }

    // �÷��̾� ���� ����
    private Timer InvincibleTimer = new Timer();

    public void SetInvincibleMode(float duration)
    {
        InvincibleTimer.ResetTimer();
        InvincibleTimer.StartTimer(); //Ÿ�̸ӽ���

        isInvincible = true;
        invincible_Duration = duration;
    }

    // Ÿ�̸� ������Ʈ �Լ�
    private void UpdateTimer()
    {
        InvincibleTimer.UpdateTimer();

        if (InvincibleTimer.GetTimer() >= invincible_Duration)
        {
            InvincibleTimer.StopTimer(); // �����ð��� ������ Ÿ�̸� ����
            isInvincible = false;
        }
    }
}
