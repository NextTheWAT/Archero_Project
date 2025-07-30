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
        fireTimer += Time.deltaTime;

        // ���� ����� �� ã��
        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = nearestEnemy != null;
        animator.SetBool("canSeeEnemy", canSee);

        bool isMoving = animator.GetBool("isMoving");

        // ��Ÿ�� ���: 1 / attackSpeed (attackSpeed�� 0�̸� �߻� �� ��)
        float fireCooldown = (playerStat != null && playerStat.attackSpeed > 0f) ? 1f / playerStat.attackSpeed : float.MaxValue;

        // ������ ��(Idle)�� ��Ÿ�Ӹ��� �ݺ� �߻� + Shooting �ִϸ��̼� Ʈ����
        if (!isMoving && canSee && fireTimer >= fireCooldown)
        {
            animator.SetTrigger("isAimingFinished");

            Vector3 dirToEnemy = (nearestEnemy.transform.position - transform.position).normalized;
            dirToEnemy.y = 0f;

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint != null ? firePoint.position : transform.position,
                Quaternion.identity
            );
            bullet.transform.forward = dirToEnemy;

            fireTimer = 0f;
        }
    }
}
