using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum ActionState
{
    Idle = 0,
    Move = 1,
    Attack = 2, // MeleeAttack
    SpecialAttack = 3,
    Die = 4,
    Spinning = 5, // RangedAttack
    Hit = 6
}

// 보스 움직임 제어 클래스 
public class BossController : MonoBehaviour
{
    // 플레이어 정보 
    public GameObject player; // 플레이어 위치 
    public PlayerStat playerStat; // 플레이어 능력치 (공격력, 체력 등)

    // 보스 정보 
    public float moveSpeed; // 보스 이동속도 (내가 정하는 값)

    public float currentHp; // 현재 체력 (내가 정하는 값)
    public float maxHp = 300; // 최대 체력 (내가 정하는 값)

    public bool isDead;

    public float followDistance; // 보스가 플레이어를 따라갈 수 있는 거리 (내가 정하는 값)
    public float attackRange; // 보스가 플레이어를 공격할 수 있는 범위 (내가 정하는 값) 
    public float rangedAttackRange; // 원거리 공격 범위 (내가 정하는 값)
    public Vector3 directionToTarget; // 보스가 플레이어를 바라보는 방향
    public float distanceToPlayer; // 보스와 플레이어 사이 거리 (계속 체크하는 값)

    public ActionState currentState; // 현재 상태 (Idle, Move, ...)

    public Rigidbody rigid;

    //public float normalAttackDelayTime; // 일반 공격 딜레이 시간 (내가 정하는 값)
    public bool isNormalAttack; // 일반 공격인지? 
    //public float specialAttackDelayTime; // 특수 공격 딜레이 시간 (내가 정하는 값)
    public bool isSpecialAttack; // 특수 공격인지? 
    //public float time;

    public Animator animator;

    public Axe axe;

    public bool isAttacking;

    public GameObject cubePrefab; // 인스펙터에서 가져오기 
    public int cubeCount;

    public bool isSpinning;

    public bool isHit;

    public Renderer[] renderers; // 여러 렌더러 지원

    private Color[] originalColors; // 원래 색상 저장

    private void Awake()
    {
        // 플레이어 오브젝트 찾기 (태그로)
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
            {
                player = obj;

                // PlayerStat 컴포넌트 가져오기
                playerStat = player.GetComponent<PlayerStat>();
                if (playerStat == null)
                {
                    Debug.LogError("PlayerStat 컴포넌트를 찾을 수 없습니다!");
                }
            }
            else
            {
                Debug.LogError("태그가 'Player'인 오브젝트를 찾을 수 없습니다!");
            }
        }

        // 나머지 컴포넌트 초기화
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        axe = GetComponentInChildren<Axe>();
        currentHp = maxHp; // 현재 체력 초기화
    }

    private void Start()
    {
        // 자식까지 포함한 모든 Renderer(메시/스키닝) 가져오기
        renderers = GetComponentsInChildren<Renderer>();
        // 원래 색상 저장
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                originalColors[i] = renderers[i].material.color;
        }
    }

    private void Update()
    {
        IsHealthZero();

        if (!isDead)
        {
            // 보스가 플레이어 좌표를 계속 체크하면서 플레이어와의 거리, 플레이어를 바라보는 방향을 구한다. 
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position); // 보스와 플레이어 사이 거리
            directionToTarget = (player.transform.position - transform.position).normalized; // 보스가 플레이어를 바라보는 방향

            // (이동처리, 공격처리)
            // 플레이어가 보스가 따라갈 수 있는 거리(followDistance)로 들어오면 
            if (isHit)
            {
                Debug.Log("Update : HandleHit실행전");
                HandleHit();
            }
            else if (distanceToPlayer <= followDistance) // 따라가는게 우선순위 
            {
                LookAtTargetAndUpdateState();
            }
            else if (distanceToPlayer <= rangedAttackRange)
            {
                //SetSpinState();
            }
            else
            {
                SetIdleState();
            }

            // (공격처리)
            HandleAttack();
        }
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case ActionState.Move:
                rigid.velocity = directionToTarget * moveSpeed;
                // 애니메이션 적용 
                break;
        }
    }

    private void LookAtTargetAndUpdateState()
    {
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        if (currentState != ActionState.Attack)
        {
            // Move 상태로 전환 
            ChangeState(ActionState.Move); // FixedUpdate에서 이동시키기 
            animator.SetInteger("State", (int)ActionState.Move);

            // 플레이어를 따라가다가 플레이어가 공격범위 내로 들어오면 
            if (distanceToPlayer <= attackRange)
            {
                // 공격모드로 전환 
                ChangeState(ActionState.Attack);
            }
        }
    }

    private void SetIdleState()
    {
        ChangeState(ActionState.Idle);
        animator.SetInteger("State", (int)ActionState.Idle);
    }

    private void SetSpinState()
    {
        ChangeState(ActionState.Spinning);
        animator.SetInteger("State", (int)ActionState.Spinning);
    }

    private void HandleHit()
    {
        // 피격 애니메이션 실행 
        //animator.SetInteger("State", (int)ActionState.Hit);

        // 보스 잠깐 멈추고 (1초간)
        //StartCoroutine(HitCoroutine());
        //StopCoroutine(HitCoroutine());
        SoundManager.Instance.Boss_SFX(1);
        StartCoroutine(FlashRed()); // 추가
    }

    //private IEnumerator HitCoroutine()
    //{
    //    rigid.velocity = Vector3.zero;

    //    yield return new WaitForSeconds(0.5f);

    //    isHit = false; // isHit = false로 해서 다시 맞을 수 있게 하기 
    //    ChangeState(ActionState.Idle); // 보스 상태를 Idle로 바꿔서 Move로 이어질 수 있게 하기 
    //}

    private IEnumerator FlashRed()
    {
        if (renderers == null || renderers.Length == 0)
            yield break;

        // 붉은색으로 변경
        foreach (var r in renderers)
        {
            if (r.material.HasProperty("_Color"))
                r.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.5f);
        isHit = false;

        // 원래 색상 복구
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                renderers[i].material.color = originalColors[i];
        }
    }

    private void HandleAttack()
    {
        if (currentState == ActionState.Attack)
        {
            if (currentHp > 50)
            {
                if (!isNormalAttack)
                {
                    // 도끼에게 애니메이션 요청 
                    // 도끼가 플레이어와 닿으면 데미지 처리
                    axe.Attack(); // 데미지 처리, 애니메이션 적용 
                }
            }
            else
            {
                if (!isSpecialAttack)
                {
                    axe.SpecialAttack(); // 데미지 처리, 애니메이션 적용 
                }
            }

            // 플레이어 피가 0이 될때까지 계속 떼리기 

            // 공격하고 있는데 플레이어가 공격 범위에서 멀어지면
            if (distanceToPlayer > attackRange) // 플레이어와의 거리가 공격 범위보다 크다는 건 공격범위에서 멀어졌다는 것. 
            {
                // 애니메이션이 끝날 때까지 기다려. 
                if (!isAttacking) // 애니메이션에 isAttacking이 시작될때 true, 끝날 때 false로 두기 
                {
                    // Idle상태로 전환 
                    ChangeState(ActionState.Idle);
                    animator.SetInteger("State", (int)ActionState.Idle);// -> 이후 Move상태로 전환됨. 
                }

                // 따라갈 수 있는 거리 안에 있으면 
                if (distanceToPlayer <= followDistance)
                {
                    // Idle상태로 전환
                    ChangeState(ActionState.Idle);
                    animator.SetInteger("State", (int)ActionState.Idle);// -> 이후 Move상태로 전환됨. 
                }
            }
        }
    }

    private void IsHealthZero()
    {
        if (currentHp <= 0.0f)
        {
            // 죽음 애니메이션
            animator.SetInteger("State", (int)ActionState.Die);
            // 움직일 수 없게 하기 
            ChangeState(ActionState.Die);
            isDead = true;
            // n초 후에 보스 게임오브젝트 삭제 
            StartCoroutine(HandleDeath());
            StopCoroutine(HandleDeath());
        }
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }

    private void ChangeState(ActionState state)
    {
        // 같은 state이면 종료 
        if (currentState == state) return;

        this.currentState = state;
    }

    private void StartDamage()
    {
        isNormalAttack = false;
        isSpecialAttack = false;
        isAttacking = true;
    }
    private void EndDamage()
    {
        // 공격 딜레이 시간 끝나면 isNormalAttack, isSpecialAttack을 false로 바꿔준다. 
        isNormalAttack = true;
        isSpecialAttack = true;
        isAttacking = false;
    }
    public void TakeDamage()
    {
        if (playerStat != null)
        {
            playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
            currentHp -= playerStat.attackPower;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //followDistance 거리 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);

        //attackRange 거리
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // rangedAttackRange 거리
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangedAttackRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isHit) return;

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("불렛에 맞음 데미지!" + playerStat.attackPower);

            TakeDamage();
            ChangeState(ActionState.Hit);
            isHit = true;
        }
    }
}
