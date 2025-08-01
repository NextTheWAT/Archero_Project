using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float fireTimer = 0f;
    private Animator animator;
    private PlayerStat playerStat;

    private bool isFirstShotAfterStop = false;
    public float firstShotDelay = 0.3f; // ���� �� ù �߻� ������

    private bool prevIsMoving = false;
    private bool waitingFirstShot = false;
    private Coroutine firstShotCoroutine = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerStat = GetComponent<PlayerStat>();
        fireTimer = 0f;
        prevIsMoving = animator.GetBool("isMoving");
    }

    void Update()
    {
        if (playerStat != null && playerStat.IsDead)
            return;

        fireTimer += Time.deltaTime;

        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = false;
        float distanceToEnemy = 0f;

        if (nearestEnemy != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            if (playerStat != null && distanceToEnemy <= playerStat.attackRange)
                canSee = true;
        }

        animator.SetBool("canSeeEnemy", canSee);

        bool isMoving = animator.GetBool("isMoving");

        // �̵��ϴٰ� ���� "����"���� �ڷ�ƾ ���� (�� �� �����Ӹ�)
        if (prevIsMoving && !isMoving)
        {
            if (!waitingFirstShot && canSee)
            {
                isFirstShotAfterStop = true;
                waitingFirstShot = true;
                animator.SetTrigger("isAimingFinished");
                if (firstShotCoroutine != null)
                    StopCoroutine(firstShotCoroutine);
                firstShotCoroutine = StartCoroutine(FirstShotDelayCoroutine());
            }
        }
        // ���� ���¿��� �ٽ� �̵��ϸ� �ڷ�ƾ ���
        else if (!prevIsMoving && isMoving)
        {
            if (firstShotCoroutine != null)
            {
                StopCoroutine(firstShotCoroutine);
                firstShotCoroutine = null;
                waitingFirstShot = false;
                isFirstShotAfterStop = false;
            }
        }
        prevIsMoving = isMoving;

        float fireCooldown = (playerStat != null && playerStat.attackSpeed > 0f) ? 1f / playerStat.attackSpeed : float.MaxValue;

        // �Ϲ� ��Ÿ�� �߻�
        if (!isMoving && canSee && !isFirstShotAfterStop && fireTimer >= fireCooldown)
        {
            animator.SetTrigger("isAimingFinished");

            Shoot(nearestEnemy);

            fireTimer = 0f;
            SoundManager.Instance.PlayerShooting_SFX();
        }
    }

    private IEnumerator FirstShotDelayCoroutine()
    {
        yield return new WaitForSeconds(firstShotDelay);

        // ���� ���� & ���� ������ ��Ÿ� ���� ���� ���� �߻�
        bool isMoving = animator.GetBool("isMoving");
        GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
        bool canSee = false;
        if (nearestEnemy != null && playerStat != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            if (distanceToEnemy <= playerStat.attackRange)
                canSee = true;
        }

        if (!isMoving && canSee)
        {
            Shoot(nearestEnemy);
            fireTimer = 0f;
            SoundManager.Instance.PlayerShooting_SFX();
        }

        isFirstShotAfterStop = false;
        waitingFirstShot = false;
        firstShotCoroutine = null;
    }

    private void Shoot(GameObject targetEnemy)
    {
        if (targetEnemy == null) return;

        Vector3 dirToEnemy = (targetEnemy.transform.position - transform.position).normalized;
        dirToEnemy.y = 0f;

        Quaternion bulletRot = Quaternion.LookRotation(dirToEnemy, Vector3.up);

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint != null ? firePoint.position : transform.position,
            bulletRot
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null && playerStat != null)
        {
            bulletScript.damage = playerStat.attackPower;
        }
    }
}
