using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionState
{
    Idle,
    Move,
    Attack
}

// 보스 움직임 제어 클래스 
public class BossMovementController : MonoBehaviour
{
    /*
     
    구현 기능 : 보스가 플레이어 따라가기 

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
     
     */
    public Transform player; // 플레이어 위치 

    public float moveSpeed; // 보스 이동속도 
    public float followDistance; // 보스 플레이어를 따라갈 수 있는 거리 (내가 정하는 것)
    public Vector3 directionToTarget; // 보스가 플레이어를 바라보는 방향
    public float distanceToPlayer; // 보스와 플레이어 사이 거리 (계속 체크하는 값)

    public ActionState currentState; // 현재 상태 (Idle, Move, ...)

    public Rigidbody rigid;

    public float health = 100;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //-보스가 플레이어 좌표를 계속 체크한다. 
        //-플레이어 보스가 따라갈 수 있는 거리(distance)로 들어오면 보스가 플레이어 위치에서 살짝 떨어진 거리까지 이동한다(서로 붙는 정도)
        
        // 보스가 플레이어 좌표를 계속 체크하면서 플레이어와의 거리, 플레이어를 바라보는 방향을 구한다. 
        distanceToPlayer = Vector3.Distance(transform.position, player.position); // 보스와 플레이어 사이 거리
        directionToTarget = (player.position - transform.position).normalized; // 보스가 플레이어를 바라보는 방향

        

        // 플레이어가 보스가 따라갈 수 있는 거리(followDistance)로 들어오면 
        if (distanceToPlayer <= followDistance)
        {
            if(currentState != ActionState.Attack)
            {
                // Move 상태로 전환 
                ChangeState(ActionState.Move); // FixedUpdate에서 이동시키기 
                // 보스가 플레이어 위치에서 살짝 떨어진 거리까지 이동한다. (서로 붙는 정도) 
            }
        }
        

        if (currentState == ActionState.Attack)
        {
            if (health > 50)
            {
                Debug.Log("일반공격"); // 일반공격
                //TakeDamage(); // 데미지 입히는 메서드 호출 
                // 애니메이션 적용
            }
            else
            {
                Debug.Log("특수공격"); // 강공격, 멀리 있으면 도끼 던지기? 
                //TakeDamage(); // 데미지 입히는 메서드 호출 
                // 애니메이션 적용
            }

            Debug.Log("플레이어 HP가 줄어듦");
            // 플레이어 피가 0이 될때까지 계속 떼리기 

            if (distanceToPlayer <= followDistance)
            {
                ChangeState(ActionState.Idle);
            }
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

    private void ChangeState(ActionState state)
    {
        // 같은 state이면 종료 
        if (currentState == state) return;

        this.currentState = state;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeState(ActionState.Attack);
        }
    }

    //private void TakeDamage(Player player)
    //{

    //}
}
