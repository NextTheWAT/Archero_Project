using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.XR;
using static MonsterFSM;

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
    private Animator anim; //Animator ���� ����
    public Transform player; //�÷��̾� ��ġ ���� ����
    private bool isAttacking = false; //���� ������ Ȯ��

    private MonsterStat stats; //MonsterStat.cs �ҷ���

    public GameObject projectilePrefab; //����ü ������
    public Transform firePoint; //����ü ���� ��ġ

    private void Start()
    {
        //�ڽ� ������Ʈ �߿� Animator ������Ʈ�� ã�Ƽ� anim�� ����
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<MonsterStat>();
        ChangeState(MonsterState.Idle); //ó�� ���´� Idle

        //Player �±� ������Ʈ ã�Ƽ� ��ġ(transform) ����
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
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

        if (distance >= stats.detectionRange)
        {
            if (currentState != MonsterState.Idle)
                ChangeState(MonsterState.Idle);
        }
        else if (distance > stats.attackRange && distance < stats.detectionRange)
        {
            if (currentState != MonsterState.Move)
                ChangeState(MonsterState.Move);
        }
        else if (distance <= stats.attackRange)
        {
            if (!isAttacking) // ���� ���� �ƴ� ���� �ڷ�ƾ ����
            {
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
            transform.Translate(Vector3.forward * stats.moveSpeed * Time.deltaTime);
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
        isAttacking = true;

        ChangeState(MonsterState.Attack);
        FireProjectile();
        yield return new WaitForSeconds(stats.attackDelay); // ���� �ִϸ��̼� ���̸�ŭ ����
        
        ChangeState(MonsterState.Idle);
        yield return new WaitForSeconds(0.5f); //���� ���ݱ��� ������

        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (stats == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stats.detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }

    void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null && player != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            MonsterProjectile projectile = proj.GetComponent<MonsterProjectile>();
            if (projectile != null)
            {
                projectile.SetTarget(player);
            }
        }
    }
}
