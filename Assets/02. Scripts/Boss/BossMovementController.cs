using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionState
{
    Idle,
    Move,
    Attack
}

// ���� ������ ���� Ŭ���� 
public class BossMovementController : MonoBehaviour
{
    /*
     
    ���� ��� : ������ �÷��̾� ���󰡱� 

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
     
     */
    public Transform player; // �÷��̾� ��ġ 

    public float moveSpeed; // ���� �̵��ӵ� 
    public float followDistance; // ���� �÷��̾ ���� �� �ִ� �Ÿ� (���� ���ϴ� ��)
    public Vector3 directionToTarget; // ������ �÷��̾ �ٶ󺸴� ����
    public float distanceToPlayer; // ������ �÷��̾� ���� �Ÿ� (��� üũ�ϴ� ��)

    public ActionState currentState; // ���� ���� (Idle, Move, ...)

    public Rigidbody rigid;

    public float health = 100;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //-������ �÷��̾� ��ǥ�� ��� üũ�Ѵ�. 
        //-�÷��̾� ������ ���� �� �ִ� �Ÿ�(distance)�� ������ ������ �÷��̾� ��ġ���� ��¦ ������ �Ÿ����� �̵��Ѵ�(���� �ٴ� ����)
        
        // ������ �÷��̾� ��ǥ�� ��� üũ�ϸ鼭 �÷��̾���� �Ÿ�, �÷��̾ �ٶ󺸴� ������ ���Ѵ�. 
        distanceToPlayer = Vector3.Distance(transform.position, player.position); // ������ �÷��̾� ���� �Ÿ�
        directionToTarget = (player.position - transform.position).normalized; // ������ �÷��̾ �ٶ󺸴� ����

        

        // �÷��̾ ������ ���� �� �ִ� �Ÿ�(followDistance)�� ������ 
        if (distanceToPlayer <= followDistance)
        {
            if(currentState != ActionState.Attack)
            {
                // Move ���·� ��ȯ 
                ChangeState(ActionState.Move); // FixedUpdate���� �̵���Ű�� 
                // ������ �÷��̾� ��ġ���� ��¦ ������ �Ÿ����� �̵��Ѵ�. (���� �ٴ� ����) 
            }
        }
        

        if (currentState == ActionState.Attack)
        {
            if (health > 50)
            {
                Debug.Log("�Ϲݰ���"); // �Ϲݰ���
                //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                // �ִϸ��̼� ����
            }
            else
            {
                Debug.Log("Ư������"); // ������, �ָ� ������ ���� ������? 
                //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                // �ִϸ��̼� ����
            }

            Debug.Log("�÷��̾� HP�� �پ��");
            // �÷��̾� �ǰ� 0�� �ɶ����� ��� ������ 

            if (distanceToPlayer <= followDistance)
            {
                ChangeState(ActionState.Idle);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeState(ActionState.Attack);
        }
    }

    //private void TakeDamage(Player player)
    //{

    //}
}
