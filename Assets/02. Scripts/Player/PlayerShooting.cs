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
        fireTimer += Time.deltaTime;

        // 가장 가까운 적 찾기
        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = nearestEnemy != null;
        animator.SetBool("canSeeEnemy", canSee);

        bool isMoving = animator.GetBool("isMoving");

        // 쿨타임 계산: 1 / attackSpeed (attackSpeed가 0이면 발사 안 함)
        float fireCooldown = (playerStat != null && playerStat.attackSpeed > 0f) ? 1f / playerStat.attackSpeed : float.MaxValue;

        // 멈췄을 때(Idle)만 쿨타임마다 반복 발사 + Shooting 애니메이션 트리거
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
