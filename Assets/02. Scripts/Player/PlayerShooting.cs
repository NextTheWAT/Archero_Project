using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireCooldown = 1f;
    public Transform firePoint;

    private float fireTimer = 0f;
    private Animator animator;
    private bool isShooting = false;
    private GameObject currentTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isMoving = animator.GetBool("isMoving");

        if (isMoving)
        {
            // 이동 중이면 공격 중단
            if (isShooting)
            {
                isShooting = false;
                currentTarget = null;
                animator.SetBool("canSeeEnemy", false);
            }
            return;
        }

        fireTimer += Time.deltaTime;

        // 가장 가까운 적 찾기
        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = nearestEnemy != null;

        animator.SetBool("canSeeEnemy", canSee);

        // Idle 상태에서만 Shooting 트리거 발동
        if (canSee && !isShooting && fireTimer >= fireCooldown)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle2")) // Idle 상태 이름에 맞게 수정
            {
                isShooting = true;
                currentTarget = nearestEnemy;
                animator.SetTrigger("isAimingFinished");
            }
        }

        // 적이 사라지면 상태 초기화
        if (!canSee && isShooting)
        {
            isShooting = false;
            currentTarget = null;
        }
    }

    // Shooting 애니메이션 끝날 때 Animation Event로 호출
    public void OnShootingFinished()
    {
        if (isShooting && currentTarget != null)
        {
            Vector3 dirToEnemy = (currentTarget.transform.position - transform.position).normalized;
            dirToEnemy.y = 0f;

            GameObject bullet = Instantiate(bulletPrefab, firePoint != null ? firePoint.position : transform.position, Quaternion.identity);
            bullet.transform.forward = dirToEnemy;

            fireTimer = 0f;
        }
        isShooting = false;
        animator.ResetTrigger("isAimingFinished");
    }
}
