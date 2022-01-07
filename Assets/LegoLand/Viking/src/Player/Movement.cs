using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("MoveSpeed")]
    private float defaultMoveSpeed; //�߰�
    public float moveSpeed;
    private float gravity = 20f;

    [Header("Rotation")]
    [SerializeField]
    private float rotateSpeed = 10.0f;

    [Header("Slide")]
    [SerializeField]
    private float _slideSpeed = 3.0f;
    public float _slopeLimit = 10.0f;
    private float _groundRayDistance = 1.0f;
    private RaycastHit _slopeHit;
    private bool risingSlope = false;

    [HideInInspector]
    public Vector3 moveDirection;
    [HideInInspector]
    public Vector3 rollDirection;
    [HideInInspector]
    public Vector3 lastDirection;

    [HideInInspector]
    public bool isRoll = false;
    public bool isCheck = false;


    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        moveDirection = Vector3.zero;
        defaultMoveSpeed = moveSpeed; //�߰�
    }

    private void Update()
    {
        UpdateTimer(); // �߰�
        UpdateSpeedUp(); // �߰�
    }
    
    public void MoveTo(Vector3 direction)
    {
        risingSlope = false;

        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
        moveDirection.y = 0.0f;

        if (isRoll)
            moveDirection = rollDirection;

        //if (!characterController.isGrounded)
        //    moveDirection.y -= gravity * Time.deltaTime;

        if (OnSteepSlope())
            SteepSlopeMovement();
        else if(!OnSteepSlope() && direction != Vector3.zero)
            lastDirection = moveDirection;
        
        Rotation(direction);

        moveDirection *= moveSpeed;
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, Vector3.zero);
        projectedVelocity.y -= 9.81f;

        if (risingSlope)
        {
            characterController.Move(projectedVelocity * Time.deltaTime);
            // characterController.Move(moveDirection * Time.deltaTime);
            return;
        }
        characterController.Move(projectedVelocity * Time.deltaTime);

        //characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void Rotation(Vector3 direction)
    {
        Vector3 targetDir = Vector3.zero;

        if(direction != Vector3.zero)
        {
            targetDir.z = moveDirection.z;
            targetDir.x += moveDirection.x;
            targetDir.y = 0.0f;
        }
        else
        {
            targetDir.z = lastDirection.z;
            targetDir.x += lastDirection.x;
            targetDir.y = 0.0f;
        }

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotateSpeed * Time.deltaTime);

        transform.rotation = targetRotation;
    }

    public void Roll(Vector3 direction)
    {
        rollDirection = transform.forward;
        rollDirection.Normalize();

        Quaternion rollRotation = Quaternion.LookRotation(rollDirection);
        transform.rotation = rollRotation;
    }

    private bool OnSteepSlope()
    {
        if (!characterController.isGrounded) return false;

        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit,
            (characterController.height / 2) + _groundRayDistance))
        {
            float _slopeAngle = Vector3.Angle(_slopeHit.normal, Vector3.up);
            return _slopeAngle > _slopeLimit;
        }

        return false;
    }

    private void SteepSlopeMovement()
    {
        Vector3 slopeDirection = Vector3.up - _slopeHit.normal * Vector3.Dot(Vector3.up, _slopeHit.normal);
        float slideSpeed = _slideSpeed + (1 - Mathf.Abs(slopeDirection.normalized.x));

        // Debug.Log("Move : " + moveDirection.normalized.z + ", Slope : " + slopeDirection.normalized.z);
        if(moveDirection.normalized.x == 0 && moveDirection.normalized.z == 0)
        {
            moveSpeed = slideSpeed;
            moveDirection = slopeDirection * -slideSpeed;
            moveDirection.y = moveDirection.y - _slopeHit.point.y;
            risingSlope = false;
            return;
        }
        else if ( (moveDirection.normalized.x < 0 && slopeDirection.normalized.x < 0 )
            || (moveDirection.normalized.x > 0 && slopeDirection.normalized.x > 0))
        {
            moveSpeed -= slideSpeed;
            risingSlope = true;
        }
        else if ((moveDirection.normalized.x < 0 && slopeDirection.normalized.x > 0)
            || (moveDirection.normalized.x > 0 && slopeDirection.normalized.x < 0))
        {
            moveSpeed += slideSpeed;
            risingSlope = false;
        }
        lastDirection = moveDirection.normalized;
    }

    //=========�߰�
    private Timer SpeedUpTimer = new Timer();

    private float speedupDuration = 0;

    public void DecreaseSpeed(float value)
    {
        moveSpeed = moveSpeed-value >=  0 ? moveSpeed - value : 0;
    }

    public void IncreaseSpeed(float value,float duration)
    {
        SpeedUpTimer.ResetTimer(); //���� Ÿ�̸�
        SpeedUpTimer.StartTimer(); // Ÿ�̸� ����

        moveSpeed += value;
        Debug.Log("moveSpeed : " + moveSpeed);
        speedupDuration = duration;
    }

    private void UpdateSpeedUp()
    {
        if (SpeedUpTimer.GetTimer() >= speedupDuration) SpeedUpTimer.StopTimer(); //�����ð����� Ÿ�̸� ����

        if (SpeedUpTimer.GetTimerStopState()) //Ÿ�̸Ӱ� ���� ���°� �ƴϸ� ���ǵ� ���
        {
            moveSpeed = defaultMoveSpeed;
           // Debug.Log("moveSpeed : " + moveSpeed);
        }
    }

    private void UpdateTimer()
    {
        SpeedUpTimer.UpdateTimer();
    }
}
