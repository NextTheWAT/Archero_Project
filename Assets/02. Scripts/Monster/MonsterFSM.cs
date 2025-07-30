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
    private Animator anim; //Animator ���� ����
    public Transform player; //�÷��̾� ��ġ ���� ����
    public float chaseRange = 10f; //�÷��̾ ���󰡴� �Ÿ�
    public float attackRange = 2f; //���� �õ� �Ÿ�

    private void Start()
    {
        //�ڽ� ������Ʈ �߿� Animator ������Ʈ�� ã�Ƽ� anim�� ����
        anim = GetComponentInChildren<Animator>();
        ChangeState(MonsterState.Idle); //ó�� ���´� Idle

        //Player �±� ������Ʈ ã�Ƽ� ��ġ(transform) ����
        GameObject playerObj = GameObject.FindWithTag("Player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update() //���� ���¿� ���� �´� �Լ� ����
    {
        CheckDistance(); //�� �����Ӹ��� �Ÿ� üũ

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

    private void CheckDistance()
    {
        //�÷��̾ ������ �ൿ ����
        if (player == null || currentState == MonsterState.Hit || currentState == MonsterState.Die)
            return;

        //�÷��̾�� �Ÿ� ����
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance >= chaseRange)
        {
            if (currentState != MonsterState.Idle)
                ChangeState(MonsterState.Idle);
        }
        else if (distance > attackRange && distance < chaseRange)
        {
            if (currentState != MonsterState.Move)
                ChangeState(MonsterState.Move);
        }
        else if (distance <= attackRange)
        {
            if (currentState != MonsterState.Attack)
            {
                Debug.Log("�� ���� ���� ����, �ڷ�ƾ ���� �õ�"); // Ȯ�ο�
                StartCoroutine(AttackRoutine());
            }
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

    void UpdateIdle() 
    {
        
    }
    void UpdateMove()
    {
        if (player == null) return; //�÷��̾� ������ �ൿ ����  
        { 
            //���� ���� ���
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; //y�� ���� ����

            //�÷��̾� �� ȸ����Ű��
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);

            //���� �̵�
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

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
    private IEnumerator AttackRoutine()
    {
        ChangeState(MonsterState.Attack);
        yield return new WaitForSeconds(0.8f); // ���� �ִϸ��̼� ���̸�ŭ ����
        ChangeState(MonsterState.Idle);
    }
}
