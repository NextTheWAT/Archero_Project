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

        // 쿨타임 계산
        float fireCooldown = (playerStat != null && playerStat.attackSpeed > 0f) ? 1f / playerStat.attackSpeed : float.MaxValue;

        // 멈췄을 때 쿨타임마다 반복 발사 + Shooting 애니메이션 트리거
        if (!isMoving && canSee && fireTimer >= fireCooldown)
        {
            animator.SetTrigger("isAimingFinished");

            Vector3 dirToEnemy = (nearestEnemy.transform.position - transform.position).normalized;
            dirToEnemy.y = 0f;

            // 화살보정
            Quaternion bulletRot = Quaternion.LookRotation(dirToEnemy, Vector3.up);

            GameObject bullet = Instantiate(
                bulletPrefab,
                firePoint != null ? firePoint.position : transform.position,
                bulletRot
            );

            fireTimer = 0f;
        }
    }
}
