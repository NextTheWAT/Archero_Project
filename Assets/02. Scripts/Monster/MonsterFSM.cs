using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MonsterFSM : MonoBehaviour
{
    public enum MonsterState //���� ���� ����
    {
        Idle,
        Move,
        Attack,
        Hit,
        Die
    }

    private MonsterState currentState; //���� ���� �����ϴ� ����
    public float moveSpeed = 2f; //�̵��ӵ�
    private Animator anim; //Animator�� ������ ����
    private IEnumerator AttackRoutine()
    {
        ChangeState(MonsterState.Attack);
        yield return new WaitForSeconds(0.8f); // ���� �ִϸ��̼� ���̸�ŭ ����
        ChangeState(MonsterState.Idle);
    }

    private void Start()
    {
        //�ڽ� ������Ʈ �߿� Animator ������Ʈ�� ã�Ƽ� anim�� ����
        anim = GetComponentInChildren<Animator>();
        ChangeState(MonsterState.Idle); //ó�� ���´� Idle
    }

    private void Update() //���� ���¿� ���� �´� �Լ� ����
    {
        //���� ��ȯ�� �ӽ� Ű �Է�
        if (currentState == MonsterState.Idle && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(MonsterState.Move);
        }
        
        //Move���¿��� AŰ ������ �۵�
        if (currentState == MonsterState.Move && Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AttackRoutine());
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeState(MonsterState.Hit);
        }
        //Move���¿��� KŰ ������ �۵�
        if (currentState != MonsterState.Die && Input.GetKeyDown(KeyCode.K))
        {
            ChangeState(MonsterState.Die);
        }

        switch (currentState)
        {
            case MonsterState.Idle:
                UpdateIdle();
                break;

            case MonsterState.Move:
                UpdateMove();
                break;

            case MonsterState.Attack:
                UpdateAttack();
                break;

            case MonsterState.Hit:
                UpdateHit();
                break;

            case MonsterState.Die:
                UpdateDie();
                break;
        }
    }

    private void ChangeState(MonsterState newState) //���¸� ��ȯ�ϴ� �Լ�
    {
        currentState = newState;

        if (anim != null) //Animator�� ���� ����
        {
            anim.SetInteger("State", (int)newState);
        }

        Debug.Log($"���� ��ȯ��: {newState}"); //���� ���� �� �α� ���
    }

    //���º� ó�� �Լ�(����)
    void UpdateIdle() { }
    void UpdateMove()
    {
        //������ ����
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void UpdateAttack()
    {
        Debug.Log("����");
    }
    void UpdateHit()
    {
        Debug.Log("������");
    }
    void UpdateDie()
    {
        Debug.Log("���");
    }
}
