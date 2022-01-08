using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public enum EnemyType { Enemy_Missile, Enemy_Arrow , Enemy_Dagger , Enemy_Boss };
    private EnemyType Type = EnemyType.Enemy_Missile;
    public GameObject[] Enemies;

    private GameObject Enemy;
    private int State = 1;
    private int State_Index = 1;

    void Start()
    {
        Enemy = Instantiate(Enemies[(int)Type], this.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)
          // ||  // ���� ������ �Ǹ� �������� Up
          )
        {
            OnChangeStage();
        }
    }

    void OnChangeStage()
    {
        if (State_Index < 4) State_Index++;
        else
        {
            OnChangeType(); // �������� �ö󰥶� ���� ���� �ٲ��
            State++;
            State_Index = 1;
        }
    }

    void OnChangeType()
    { 
        Destroy(Enemy);
        int type = (int)Type;
        if(type < Enemies.Length-1) type++;
        Type = (EnemyType)type;
        Enemy = Instantiate(Enemies[type], this.transform);
    }

    void OnAttack()
    {
        switch (State)
        {
            case 1: // �̻���
                {
                    switch (State)
                    {
                        case 1: { }break;
                        case 2: { }break;
                        case 3: { }break;
                        case 4: { }break;
                    }
                } break;
            case 2: //ȭ��
                {
                    switch (State)
                    {
                        case 1: { } break;
                        case 2: { } break;
                        case 3: { } break;
                        case 4: { } break;
                    }
                } break;
            case 3: //ǥâ
                {

                } break;
            case 4: { }break;
        }
    }
}
