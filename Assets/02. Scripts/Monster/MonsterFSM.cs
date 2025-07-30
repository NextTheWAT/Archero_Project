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
    private Animator anim; //Animator를 저장할 변수
    private IEnumerator AttackRoutine()
    {
        ChangeState(MonsterState.Attack);
        yield return new WaitForSeconds(0.8f); // 공격 애니메이션 길이만큼 유지
        ChangeState(MonsterState.Idle);
    }

    private void Start()
    {
        //자식 오브젝트 중에 Animator 컴포넌트를 찾아서 anim에 저장
        anim = GetComponentInChildren<Animator>();
        ChangeState(MonsterState.Idle); //처음 상태는 Idle
    }

    private void Update() //현재 상태에 따라 맞는 함수 실행
    {
        //상태 전환용 임시 키 입력
        if (currentState == MonsterState.Idle && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(MonsterState.Move);
        }
        
        //Move상태에서 A키 눌러야 작동
        if (currentState == MonsterState.Move && Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AttackRoutine());
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeState(MonsterState.Hit);
        }
        //Move상태에서 K키 눌러야 작동
        if (currentState != MonsterState.Die && Input.GetKeyDown(KeyCode.K))
        {
            ChangeState(MonsterState.Die);
        }

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

    private void ChangeState(MonsterState newState) //상태를 전환하는 함수
    {
        currentState = newState;

        if (anim != null) //Animator에 상태 전달
        {
            anim.SetInteger("State", (int)newState);
        }

        Debug.Log($"상태 전환됨: {newState}"); //상태 변경 시 로그 출력
    }

    //상태별 처리 함수(예정)
    void UpdateIdle() { }
    void UpdateMove()
    {
        //앞으로 전진
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
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
}
