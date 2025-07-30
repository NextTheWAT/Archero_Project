using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    /*
    
    ���� ���� Ŭ����.
    �ʿ��� �͵� : 
    - �÷��̾ �����Ÿ��� ���� ���󰣴� 
    - �÷��̾ �����Ѵ� (�Ϲ� ����, Ư������) 
    - ���ݿ��� �Ϲݰ��ݰ� Ư�������� �ִ�. (�ָ� ������ ����������? ������ ������ �Ϲݰ���) <��������>
    - �Ϲ� ���ͺ��� ü���� ���� �����Ѵ�, 

    �ؾ��� �� 
    1. �÷��̾� ��ġ ��������
    2. ������ �÷��̾� ���󰡰� �ϱ�

     */

    public Transform player; // �÷��̾� ��ġ 
    public float followRange = 5; // �����Ÿ� 
    public float attackRange = 1; // ���ݹ��� 
    public bool isAttacking = false;
    public Vector3 movementDirection; // ������ �����̴� ���� 

    private Rigidbody _rigid;
    private Animator _animator;

    public bool isMoving;

    // ���� Ư��
    public float attackPower = 20f;
    public float moveSpeed = 5f;
    public float attackSpeed = 1f;
    public int maxHp = 200;
    public int currentHp = 200;
    public int projectileCount = 1;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // ������ �÷��̾� �Ÿ��� 3�̳��̸� ������ �÷��̾ ���󰡰� �Ѵ�. 
        // �̰� ��� �ڵ�� ǥ������? 
        // TopDown RPG ���ǿ��� �� �� ����. �ڵ带 �����غ���. 
        // ������ �÷��̾ �����ϰ� ���󰡰� �ϴ� ���� ã�� 
        // �� �̰���? ���� ���Ͽ� ���ԵǴϱ�. ������ ���󰡼� ������ �ؾ��ϴϱ�. 

        // Ÿ�ٰ��� �Ÿ�, ���� �������� 
        float distanceToTarget = Vector3.Distance(transform.position, player.position); // �÷��̾���� �Ÿ�
        Vector3 directionToTarget = (player.position - transform.position).normalized; // ������ �÷��̾ �ٶ󺸴� ����

        isAttacking = false; // ���߿� ���� 

        // �÷��̾���� �Ÿ��� �����Ÿ�(followRange) ������ �������� 
        if(distanceToTarget <= followRange)
        {
            // �÷��̾���� �Ÿ��� ���ݹ���(attackRange) ������ �������� 
            if(distanceToTarget < attackRange)
            {
                // �÷��̾��� ���̾��ũ�� �����ͼ� 
                //int layerMaskTarget = 
                // -> �̹� �÷��̾�

                // ������ ������ �� �ִ� ������Ʈ���? 
                // ���� 
                // ���߿� ���� 
                Debug.Log("���߰�");
                Debug.Log("��������");
                isMoving = false;
                isAttacking = true;
            }

            // �̵���Ű��. 
            // ��� �̵�����? �̵������� �������༭ FixedUpdate���� �����̵� ó�� 
            isMoving = true;
            movementDirection = directionToTarget;
        }
    }

    private void FixedUpdate()
    {
        if(isMoving)
            Movement(movementDirection);
    }
    
    private void Movement(Vector3 direction)
    {
        direction = direction * moveSpeed; // �̵�����, �ӵ� ����

        // �̵�
        _rigid.velocity = direction; // �̵� ���� 
        _animator.SetInteger("State", 1); // �ִϸ��̼� ���� 
    }
}
