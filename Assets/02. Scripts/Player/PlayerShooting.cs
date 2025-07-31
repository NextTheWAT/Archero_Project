using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireTimer = 0f;
    private Animator animator;
    private PlayerStat playerStat; // PlayerStat ����

    void Start()
    {
        animator = GetComponent<Animator>();
        playerStat = GetComponent<PlayerStat>(); // PlayerStat �Ҵ�
    }

    void Update()
    {
        // �÷��̾ �׾����� �ƹ��͵� ���� ����
        if (playerStat != null && playerStat.IsDead)
            return;

        fireTimer += Time.deltaTime;

        // ���� ����� �� ã��
        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = false;
        float distanceToEnemy = 0f;

        if (nearestEnemy != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            // ��Ÿ� �̳��� ���� ������ canSee = true
            if (playerStat != null && distanceToEnemy <= playerStat.attackRange)
                canSee = true;
        }

        animator.SetBool("canSeeEnemy", canSee);

        bool isMoving = animator.GetBool("isMoving");

        // ��Ÿ�� ���
        float fireCooldown = (playerStat != null && playerStat.attackSpeed > 0f) ? 1f / playerStat.attackSpeed : float.MaxValue;

        // ������ �� ��Ÿ�Ӹ��� �ݺ� �߻� + Shooting �ִϸ��̼� Ʈ����
        if (!isMoving && canSee && fireTimer >= fireCooldown)
        {
            animator.SetTrigger("isAimingFinished");

            Vector3 dirToEnemy = (nearestEnemy.transform.position - transform.position).normalized;
            dirToEnemy.y = 0f;

            Quaternion bulletRot = Quaternion.LookRotation(dirToEnemy, Vector3.up);

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint != null ? firePoint.position : transform.position,
                bulletRot
            );

            // PlayerStat�� ���ݷ�(attackPower)�� Bullet�� damage�� �Ҵ�
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null && playerStat != null)
            {
                bulletScript.damage = playerStat.attackPower;
            }

            fireTimer = 0f;
        }
    }
}
