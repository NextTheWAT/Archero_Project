using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    public bool isAnimDamage = false;
    public bool isDie = false;

    private MonsterParticleControl particleControl;

    private void Start()
    {
        //�ڽ� ������Ʈ �߿� Animator ������Ʈ�� ã�Ƽ� anim�� ����
        anim = GetComponent<Animator>();
        stats = GetComponent<MonsterStat>();
        ChangeState(MonsterState.Idle); //ó�� ���´� Idle

        //Player �±� ������Ʈ ã�Ƽ� ��ġ(transform) ����
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        particleControl = GetComponent<MonsterParticleControl>();
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
        if(stats.currentHp <= 0)
        {
            ChangeState(MonsterState.Die);
            return;
        }

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
        SoundManager.Instance.Monster_SFX(0); // ���� ���� ���� ���
        if (player == null) return;
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);

            Debug.Log("������");
        }
    }
    void UpdateHit()
    {
        Debug.Log("������");
    }
    void UpdateDie()
    {
        if (!isDie)
        {
            isDie = true;
            Debug.Log("���� ���");
            SoundManager.Instance.Monster_SFX(2);

            // �±� ����
            gameObject.tag = "Untagged";

            // ���̾ DeadMonster�� ����
            gameObject.layer = LayerMask.NameToLayer("DeadMonster");

            Destroy(gameObject, 3f);
        }
    }
    private IEnumerator AttackRoutine()
    {
        isAttacking = true;

        ChangeState(MonsterState.Attack);
        //FireProjectile();
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
        SoundManager.Instance.Player_SFX(1); // ���� ���� ���
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

    private void Awake()
    {
        // ��ƼŬ ��Ʈ�� ��������
        particleControl = GetComponent<MonsterParticleControl>();
    }

    public void TakeDamage(float damage)
    {
        if (currentState == MonsterState.Die) return; //�̹� �׾����� ����
        stats.currentHp -= damage;

        if (stats.currentHp <= 0)
        {
            stats.currentHp = 0;
            ChangeState(MonsterState.Die);
        }
        else
        {
            ChangeState(MonsterState.Hit);
        }
    }

        public void AnimDamageStart()
    {
        isAnimDamage = false;
    }
    public void AnimDamageEnd()
    {
        isAnimDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // �ǰ� ��ġ = �Ѿ��� ���� ��ġ
            Vector3 hitPosition = other.transform.position;

            // ȸ�� = �Ѿ� ���� + X������ 180�� ȸ��
            Quaternion hitRotation = Quaternion.LookRotation(other.transform.forward) * Quaternion.Euler(180f, 0f, 0f);

            particleControl.SpawnHitParticle(hitPosition, hitRotation);
        }
    }



}
