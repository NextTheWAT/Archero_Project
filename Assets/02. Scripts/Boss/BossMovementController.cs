using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionState
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    SpecialAttack = 3
}

// ���� ������ ���� Ŭ���� 
public class BossMovementController : MonoBehaviour
{
    /*
     
    ���� ���1 : ������ �÷��̾� ���󰡱� 

    �ʿ��� �͵� 
    - �÷��̾� ���󰡱� (O)
        - ������ �÷��̾� ��ǥ�� ��� üũ�Ѵ�. 
        - �÷��̾� ������ ���� �� �ִ� �Ÿ�(distance)�� ������ ������ �÷��̾� ��ġ���� ��¦ ������ �Ÿ����� �̵��Ѵ� (���� �ٴ� ����)
    - �÷��̾� ��ǥ (O)
    - ���� �̵��ӵ� (O)
    - ���� �̵� �ִϸ��̼� ���� (�߿�X)
    - ���� : ĳ���Ϳ� �پ��� ���� ���߰� �����ؾ��ϴµ� ��� �̵��� ��. 
     -> ĳ���Ϳ� �پ��� �� ���ݻ��·� �ٲٰ� ���ݷα׸� ����. 

    ���� ���
    - �÷��̾� ��ǥ�� �޾ƿ´�
    - ������ �÷��̾ �ν��ؼ� ���� �Ÿ��� ���صд�.
    - ������ �÷��̾��� �Ÿ��� ����Ѵ�. 
    - ������ �÷��̾ �ٶ󺸴� ������ ����Ѵ�. 
    - ����� �Ÿ��� ������ �Ÿ��� ���ؼ� �Ÿ� ���� ������ ������ �̵���Ų��. 


    ���� ���2 : ���� ������ �ð� 
    ���� ������ ActionState.Attack ������ �� �Ϲݰ����̳� Ư�������� ��� ��. 
    ������ ��� ���� �÷��̾� hp�� �������� ���̴ϱ� ���� �뷱���� �� �°� �̻���
    ���ݿ� ������ �ð��� �־ ������ ��� ���� ���ϰ� ������. 

    �ʿ��� �͵� 
    - ���� ������ �ð� ���� 
        - �Ϲ� ���� ������ �ð�(normalAttackDelayTime)
        - Ư�� ���� ������ �ð�(specialAttackDelayTime)
    - ������ ���۵Ǹ� ���� Ÿ��(�Ϲ� ����, Ư�� ����)�� �°� ������ �ð��� �ο��ؼ�
      ������ �ð� ���ȿ��� ������ �� ���� ����� 

    ���� ���
    - ���� ������ �ð� ������ �����. 
    - ���� ������ ������ ������ �߰����ش�. 


    ���� ���3 : ���� �ִϸ��̼� ���� 
    ������ ���� ��, ������ ��, ������ �� �ִϸ��̼��� ����

    �ʿ��� �͵�
    - �ִϸ�����
    - �ִϸ��̼� ���� (�ִϸ��̼� �߰�, �Ķ���� �߰�, Ʈ������ ����)
    - �ִϸ��̼� ���� �ۼ� 

    ���� ���
    - �ִϸ����� ���̱�
    - ��Ʈ�ѷ� ���� (�ִϸ��̼� �߰�, �Ķ���� �߰�, Ʈ������ ����)
    - �ִϸ��̼� ���� ���� ã�Ƽ� �ִϸ��̼� �ڵ� �߰� 

    ������ 
    - ������ 6�ù������θ� ������. 
     
     */

    // �÷��̾� ���� 
    public Transform player; // �÷��̾� ��ġ 

    // ���� ���� 
    public float moveSpeed; // ���� �̵��ӵ� (���� ���ϴ� ��)
    public float health = 100;

    public float followDistance; // ������ �÷��̾ ���� �� �ִ� �Ÿ� (���� ���ϴ� ��)
    public float attackRange; // ������ �÷��̾ ������ �� �ִ� ���� (���� ���ϴ� ��) 
    public Vector3 directionToTarget; // ������ �÷��̾ �ٶ󺸴� ����
    public float distanceToPlayer; // ������ �÷��̾� ���� �Ÿ� (��� üũ�ϴ� ��)

    public ActionState currentState; // ���� ���� (Idle, Move, ...)

    public Rigidbody rigid;

    public float normalAttackDelayTime; // �Ϲ� ���� ������ �ð� (���� ���ϴ� ��)
    public bool isNormalAttack; // �Ϲ� ��������? 
    public float specialAttackDelayTime; // Ư�� ���� ������ �ð� (���� ���ϴ� ��)
    public bool isSpecialAttack; // Ư�� ��������? 
    public float time;

    public Animator animator;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // ������ �÷��̾� ��ǥ�� ��� üũ�ϸ鼭 �÷��̾���� �Ÿ�, �÷��̾ �ٶ󺸴� ������ ���Ѵ�. 
        distanceToPlayer = Vector3.Distance(transform.position, player.position); // ������ �÷��̾� ���� �Ÿ�
        directionToTarget = (player.position - transform.position).normalized; // ������ �÷��̾ �ٶ󺸴� ����

        // (�̵�ó��, ����ó��)
        // �÷��̾ ������ ���� �� �ִ� �Ÿ�(followDistance)�� ������ 
        if (distanceToPlayer <= followDistance)
        {
            if(currentState != ActionState.Attack)
            {
                // Move ���·� ��ȯ 
                ChangeState(ActionState.Move); // FixedUpdate���� �̵���Ű�� 
                animator.SetInteger("State", (int)ActionState.Move);

                // �÷��̾ ���󰡴ٰ� �÷��̾ ���ݹ��� ���� ������ 
                if(distanceToPlayer <= attackRange)
                {
                    // ���ݸ��� ��ȯ 
                    ChangeState(ActionState.Attack);
                }
            }
        }
        else
        {
            ChangeState(ActionState.Idle);
            animator.SetInteger("State", (int)ActionState.Idle);
        }

        // (����ó��)
        if (currentState == ActionState.Attack)
        {
            if (health > 50)
            {
                if (!isNormalAttack)
                {
                    animator.SetInteger("State", (int)ActionState.Attack);
                    Debug.Log("�Ϲݰ���"); // �Ϲݰ���
                    //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                    isNormalAttack = true; // ������ �ð� �ο� 
                    // �ִϸ��̼� ����
                }
            }
            else
            {
                if (!isSpecialAttack)
                {
                    animator.SetInteger("State", (int)ActionState.SpecialAttack);
                    Debug.Log("Ư������"); // ������, �ָ� ������ ���� ������? 
                    //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                    isSpecialAttack = true; // ������ �ð� �ο� 
                    // �ִϸ��̼� ����
                }
            }

            //Debug.Log("�÷��̾� HP�� �پ��");
            // �÷��̾� �ǰ� 0�� �ɶ����� ��� ������ 

            // �����ϰ� �ִµ� �÷��̾ ���� �������� �־�����
            if (distanceToPlayer > attackRange) // �÷��̾���� �Ÿ��� ���� �������� ũ�ٴ� �� ���ݹ������� �־����ٴ� ��. 
            {
                // Idle���·� ��ȯ 
                ChangeState(ActionState.Idle);
                animator.SetInteger("State", (int)ActionState.Idle);
            }
        }

        // �Ϲ� ���� ������ 
        if (isNormalAttack)
        {
            time += Time.deltaTime;
            if(time >= normalAttackDelayTime) 
            {
                Debug.Log($"�Ϲ� ���� ������ ��. \n�ð� = {time}");
                time = 0f;
                isNormalAttack = false;
            }
        }

        // Ư�� ���� ������
        if (isSpecialAttack)
        {
            time += Time.deltaTime;
            if(time >= specialAttackDelayTime)
            {
                Debug.Log($"Ư�� ���� ������ ��. \n�ð� = {time}");
                time = 0f;
                isSpecialAttack = false;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case ActionState.Move:
                rigid.velocity = directionToTarget * moveSpeed;
                // �ִϸ��̼� ���� 
                break;
        }
    }

    private void ChangeState(ActionState state)
    {
        // ���� state�̸� ���� 
        if (currentState == state) return;

        this.currentState = state;
    }

    //private void TakeDamage(Player player)
    //{

    //}
}
