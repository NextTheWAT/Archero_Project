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
    public float firstShotDelay = 0.3f; // 멈춘 후 첫 발사 딜레이

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

        // 이동하다가 멈춘 "순간"에만 코루틴 시작 (딱 한 프레임만)
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
        // 멈춘 상태에서 다시 이동하면 코루틴 취소
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

        // 일반 쿨타임 발사
        if (!isMoving && canSee && !isFirstShotAfterStop && fireTimer >= fireCooldown)
        {
            animator.SetTrigger("isAimingFinished");

            Shoot(nearestEnemy);

            fireTimer = 0f;
        }
    }

    private IEnumerator FirstShotDelayCoroutine()
    {
        yield return new WaitForSeconds(firstShotDelay);

        // 멈춘 상태 & 적이 여전히 사거리 내에 있을 때만 발사
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
        }

        isFirstShotAfterStop = false;
        waitingFirstShot = false;
        firstShotCoroutine = null;
    }

    private void Shoot(GameObject targetEnemy)
    {
        if (playerStat == null) return;

        SoundManager.Instance.Player_SFX(1);

        int count = Mathf.Max(1, playerStat.projectileCount);

        // 적을 향한 방향의 y값을 0으로 만들어 수평 방향만 사용
        Vector3 toEnemy = (targetEnemy.transform.position - (firePoint != null ? firePoint.position : transform.position));
        toEnemy.y = 0f;
        Vector3 forward = toEnemy.normalized;
        Quaternion bulletRot = Quaternion.LookRotation(forward, Vector3.up);

        // firePoint의 오른쪽 방향 벡터 (수평면에서만)
        Vector3 right = Vector3.Cross(Vector3.up, forward).normalized;
        float spacing = 0.5f; // 화살 간 간격(필요시 조정)

        float startOffset = -(count - 1) * spacing * 0.5f;

        for (int i = 0; i < count; i++)
        {
            // 가로로만 오프셋, 진행 방향은 모두 동일(수평)
            Vector3 offset = right * (startOffset + i * spacing);
            Vector3 spawnPos = (firePoint != null ? firePoint.position : transform.position) + offset;

            GameObject bullet = Instantiate(
                bulletPrefab,
                spawnPos,
                bulletRot
            );

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = playerStat.attackPower;
            }
        }
    }
}
