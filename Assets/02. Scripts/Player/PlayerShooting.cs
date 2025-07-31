using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireTimer = 0f;
    private Animator animator;
    private PlayerStat playerStat; // PlayerStat 참조

    void Start()
    {
        animator = GetComponent<Animator>();
        playerStat = GetComponent<PlayerStat>(); // PlayerStat 할당
    }

    void Update()
    {
        // 플레이어가 죽었으면 아무것도 하지 않음
        if (playerStat != null && playerStat.IsDead)
            return;

        fireTimer += Time.deltaTime;

        // 가장 가까운 적 찾기
        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = false;
        float distanceToEnemy = 0f;

        if (nearestEnemy != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            // 사거리 이내에 적이 있으면 canSee = true
            if (playerStat != null && distanceToEnemy <= playerStat.attackRange)
                canSee = true;
        }

        animator.SetBool("canSeeEnemy", canSee);

        bool isMoving = animator.GetBool("isMoving");

        // 쿨타임 계산
        float fireCooldown = (playerStat != null && playerStat.attackSpeed > 0f) ? 1f / playerStat.attackSpeed : float.MaxValue;

        // 멈췄을 때 쿨타임마다 반복 발사 + Shooting 애니메이션 트리거
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

            // PlayerStat의 공격력(attackPower)을 Bullet의 damage에 할당
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null && playerStat != null)
            {
                bulletScript.damage = playerStat.attackPower;
            }

            fireTimer = 0f;
        }
    }
}
