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
            // �̵� ���̸� ���� �ߴ�
            if (isShooting)
            {
                isShooting = false;
                currentTarget = null;
                animator.SetBool("canSeeEnemy", false);
            }
            return;
        }

        fireTimer += Time.deltaTime;

        // ���� ����� �� ã��
        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = nearestEnemy != null;

        animator.SetBool("canSeeEnemy", canSee);

        // Idle ���¿����� Shooting Ʈ���� �ߵ�
        if (canSee && !isShooting && fireTimer >= fireCooldown)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle2")) // Idle ���� �̸��� �°� ����
            {
                isShooting = true;
                currentTarget = nearestEnemy;
                animator.SetTrigger("isAimingFinished");
            }
        }

        // ���� ������� ���� �ʱ�ȭ
        if (!canSee && isShooting)
        {
            isShooting = false;
            currentTarget = null;
        }
    }

    // Shooting �ִϸ��̼� ���� �� Animation Event�� ȣ��
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
