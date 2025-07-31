﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public enum ActionState
{
    Idle = 0,
    Move = 1,
    Attack = 2,
    SpecialAttack = 3,
    Die = 4
}

// 보스 움직임 제어 클래스 
public class BossController : MonoBehaviour
{
    /*
     
    구현 기능1 : 보스가 플레이어 따라가기 

    필요한 것들 
    - 플레이어 따라가기 (O)
        - 보스가 플레이어 좌표를 계속 체크한다. 
        - 플레이어 보스가 따라갈 수 있는 거리(distance)로 들어오면 보스가 플레이어 위치에서 살짝 떨어진 거리까지 이동한다 (서로 붙는 정도)
    - 플레이어 좌표 (O)
    - 보스 이동속도 (O)
    - 보스 이동 애니메이션 적용 (중요X)
    - 문제 : 캐릭터와 붙었을 때는 멈추고 공격해야하는데 계속 이동만 함. 
     -> 캐릭터와 붙었을 때 공격상태로 바꾸고 공격로그를 찍어보자. 

    구현 방법
    - 플레이어 좌표를 받아온다
    - 보스가 플레이어를 인식해서 따라갈 거리를 정해둔다.
    - 보스와 플레이어의 거리를 계산한다. 
    - 보스가 플레이어를 바라보는 방향을 계산한다. 
    - 계산한 거리와 보스의 거리를 비교해서 거리 내로 들어오면 보스를 이동시킨다. 


    구현 기능2 : 공격 딜레이 시간 
    지금 구조는 ActionState.Attack 상태일 때 일반공격이나 특수공격을 계속 함. 
    공격이 계속 들어가면 플레이어 hp가 연속으로 깎이니까 게임 밸런스가 안 맞고 이상함
    공격에 딜레이 시간을 넣어서 공격이 계속 들어가지 못하게 만들자. 

    필요한 것들 
    - 공격 딜레이 시간 변수 
        - 일반 공격 딜레이 시간(normalAttackDelayTime)
        - 특수 공격 딜레이 시간(specialAttackDelayTime)
    - 공격이 시작되면 공격 타입(일반 공격, 특수 공격)에 맞게 딜레이 시간을 부여해서
      딜레이 시간 동안에는 공격할 수 없게 만들기 

    구현 방법
    - 공격 딜레이 시간 변수를 만든다. 
    - 공격 시점에 딜레이 로직을 추가해준다. 


    구현 기능3 : 보스 애니메이션 구현 
    가만히 있을 때, 움직일 때, 공격할 때 애니메이션을 구현

    필요한 것들
    - 애니메이터
    - 애니메이션 설정 (애니메이션 추가, 파라미터 추가, 트랜지션 연결)
    - 애니메이션 로직 작성 

    구현 방법
    - 애니메이터 붙이기
    - 컨트롤러 설정 (애니메이션 추가, 파라미터 추가, 트랜지션 연결)
    - 애니메이션 동작 시점 찾아서 애니메이션 코드 추가 

    문제점 
    - 방향이 6시방향으로만 고정됨. -> 보스가 따라가야 할때 Quaternion.Slerp으로 y축 회전할 수 있도록 수정 
     

    구현 기능4 : 특정 방에서 보스 등장 로직 구성
    씬이 로드되었을 때, 보스가 생성되도록 만들기 

    필요한 것들
    - Instantiate로 씬이 로드되면 자동으로 생성되게 하기 

    구현 방법 
    - 씬에 매니저 오브젝트를 만들고 매니저 스크립트를 붙이기
    - 씬에서 보스, 플레이어를 따로 두지 않고 매니저 오브젝트에서 자동으로 생성하도록 스크립트 짜기 


    구현 기능5 : 플레이어 데미지 입히기 

    필요한 것들 


     */

    // 플레이어 정보 
    public Transform player; // 플레이어 위치 

    // 보스 정보 
    public float moveSpeed; // 보스 이동속도 (내가 정하는 값)
    public float health = 100;
    public bool isDead;

    public float followDistance; // 보스가 플레이어를 따라갈 수 있는 거리 (내가 정하는 값)
    public float attackRange; // 보스가 플레이어를 공격할 수 있는 범위 (개가 정하는 값) 
    public Vector3 directionToTarget; // 보스가 플레이어를 바라보는 방향
    public float distanceToPlayer; // 보스와 플레이어 사이 거리 (계속 체크하는 값)

    public ActionState currentState; // 현재 상태 (Idle, Move, ...)

    public Rigidbody rigid;

    public float normalAttackDelayTime; // 일반 공격 딜레이 시간 (내가 정하는 값)
    public bool isNormalAttack; // 일반 공격인지? 
    public float specialAttackDelayTime; // 특수 공격 딜레이 시간 (내가 정하는 값)
    public bool isSpecialAttack; // 특수 공격인지? 
    public float time;

    public Animator animator;

    public Axe axe;

    public bool isAttacking;

    private void Awake()
    {
        // 자동으로 태그가 "Player"인 오브젝트의 PlayerStat 컴포넌트 찾기
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                player = obj.GetComponent<Transform>();
        }
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        axe = GetComponentInChildren<Axe>();
    }

    private void Update()
    {
        IsHealthZero();

        if (!isDead)
        {
            // 보스가 플레이어 좌표를 계속 체크하면서 플레이어와의 거리, 플레이어를 바라보는 방향을 구한다. 
            distanceToPlayer = Vector3.Distance(transform.position, player.position); // 보스와 플레이어 사이 거리
            directionToTarget = (player.position - transform.position).normalized; // 보스가 플레이어를 바라보는 방향

            // (이동처리, 공격처리)
            // 플레이어가 보스가 따라갈 수 있는 거리(followDistance)로 들어오면 
            if (distanceToPlayer <= followDistance)
            {
                LookAtTargetAndUpdateState();
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

        // Change
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

    private void HandleAttack()
    {
        if (currentState == ActionState.Attack)
        {
            if (health > 50)
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
                // 만약 애니메이션이 끝날 때까지 기다려. 
                if (!isAttacking) // 애니메이션에 isAttacking이 시작될때 true, 끝날 때 false로 두기 
                {
                    // Idle상태로 전환 
                    ChangeState(ActionState.Idle);
                    animator.SetInteger("State", (int)ActionState.Idle);
                }

            }
        }
    }

    private void IsHealthZero()
    {
        if(health <= 0.0f)
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

    private void OnDrawGizmosSelected()
    {
        //followDistance 거리 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,followDistance);

        //attackRange 거리
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
