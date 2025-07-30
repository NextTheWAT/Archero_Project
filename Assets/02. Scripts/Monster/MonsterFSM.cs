using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MonsterFSM : MonoBehaviour
{
    public enum MonsterState //몬스터 상태 정의
    {
        Idle,
        Move,
        Attack,
        Hit,
        Die
    }

    private MonsterState currentState; //현재 상태 저장하는 변수
    public float moveSpeed = 2f; //이동속도
    private Animator anim; //Animator 저장 변수
    public Transform player; //플레이어 위치 저장 변수
    public float chaseRange = 10f; //플레이어를 따라가는 거리
    public float attackRange = 2f; //공격 시도 거리

    private void Start()
    {
        //자식 오브젝트 중에 Animator 컴포넌트를 찾아서 anim에 저장
        anim = GetComponentInChildren<Animator>();
        ChangeState(MonsterState.Idle); //처음 상태는 Idle

        //Player 태그 오브젝트 찾아서 위치(transform) 저장
        GameObject playerObj = GameObject.FindWithTag("Player");
        if(playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update() //현재 상태에 따라 맞는 함수 실행
    {
        CheckDistance(); //매 프레임마다 거리 체크

        switch (currentState)
        {
            case MonsterState.Idle:
                UpdateIdle();
                break;

            case MonsterState.Move:
                UpdateMove();
                break;

            case MonsterState.Attack:
                UpdateAttack();
                break;

            case MonsterState.Hit:
                UpdateHit();
                break;

            case MonsterState.Die:
                UpdateDie();
                break;
        }
    }

    private void CheckDistance()
    {
        //플레이어가 없으면 행동 안함
        if (player == null || currentState == MonsterState.Hit || currentState == MonsterState.Die)
            return;

        //플레이어와 거리 측정
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance >= chaseRange)
        {
            if (currentState != MonsterState.Idle)
                ChangeState(MonsterState.Idle);
        }
        else if (distance > attackRange && distance < chaseRange)
        {
            if (currentState != MonsterState.Move)
                ChangeState(MonsterState.Move);
        }
        else if (distance <= attackRange)
        {
            if (currentState != MonsterState.Attack)
            {
                Debug.Log("▶ 공격 조건 충족, 코루틴 실행 시도"); // 확인용
                StartCoroutine(AttackRoutine());
            }
        }
    }

    private void ChangeState(MonsterState newState) //상태를 전환하는 함수
    {
        currentState = newState;

        if (anim != null) //Animator에 상태 전달
        {
            anim.SetInteger("State", (int)newState);
        }

        Debug.Log($"상태 전환됨: {newState}"); //상태 변경 시 로그 출력
    }

    void UpdateIdle() 
    {
        
    }
    void UpdateMove()
    {
        if (player == null) return; //플레이어 없으면 행동 안함  
        { 
            //방향 벡터 계산
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0f; //y축 방향 제거

            //플레이어 쪽 회전시키기
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f * Time.deltaTime);

            //앞쪽 이동
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

    }
    void UpdateAttack()
    {
        Debug.Log("공격");
    }
    void UpdateHit()
    {
        Debug.Log("데미지");
    }
    void UpdateDie()
    {
        Debug.Log("사망");
    }
    private IEnumerator AttackRoutine()
    {
        ChangeState(MonsterState.Attack);
        yield return new WaitForSeconds(0.8f); // 공격 애니메이션 길이만큼 유지
        ChangeState(MonsterState.Idle);
    }
}
