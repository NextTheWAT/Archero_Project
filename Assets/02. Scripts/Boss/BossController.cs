using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    /*
    
    보스 조작 클래스.
    필요한 것들 : 
    - 플레이어가 일정거리로 오면 따라간다 
    - 플레이어를 공격한다 (일반 공격, 특수공격) 
    - 공격에는 일반공격과 특수공격이 있다. (멀리 있으면 도끼던지기? 가까이 있으면 일반공격) <공격패턴>
    - 일반 몬스터보다 체력을 높게 설정한다, 

    해야할 일 
    1. 플레이어 위치 가져오기
    2. 보스가 플레이어 따라가게 하기

     */

    public Transform player; // 플레이어 위치 
    public float followRange = 5; // 사정거리 
    public float attackRange = 1; // 공격범위 
    public bool isAttacking = false;
    public Vector3 movementDirection; // 보스가 움직이는 방향 

    private Rigidbody _rigid;
    private Animator _animator;

    public bool isMoving;

    // 보스 특성
    public float attackPower = 20f;
    public float moveSpeed = 5f;
    public float attackSpeed = 1f;
    public int maxHp = 200;
    public int currentHp = 200;
    public int projectileCount = 1;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // 보스와 플레이어 거리가 3이내이면 보스가 플레이어를 따라가게 한다. 
        // 이걸 어떻게 코드로 표현하지? 
        // TopDown RPG 강의에서 들어본 것 같음. 코드를 참고해보자. 
        // 보스가 플레이어를 감지하고 따라가게 하는 로직 찾기 
        // 왜 이걸해? 공격 패턴에 포함되니까. 보스가 따라가서 공격을 해야하니까. 

        // 타겟과의 거리, 방향 가져오기 
        float distanceToTarget = Vector3.Distance(transform.position, player.position); // 플레이어와의 거리
        Vector3 directionToTarget = (player.position - transform.position).normalized; // 보스가 플레이어를 바라보는 방향

        isAttacking = false; // 나중에 적용 

        // 플레이어와의 거리가 사정거리(followRange) 안으로 들어왔으면 
        if(distanceToTarget <= followRange)
        {
            // 플레이어와의 거리가 공격범위(attackRange) 안으로 들어왔으면 
            if(distanceToTarget < attackRange)
            {
                // 플레이어의 레이어마스크를 가져와서 
                //int layerMaskTarget = 
                // -> 이미 플레이어

                // 보스가 공격할 수 있는 오브젝트라면? 
                // 공격 
                // 나중에 구현 
                Debug.Log("멈추고");
                Debug.Log("공격하자");
                isMoving = false;
                isAttacking = true;
            }

            // 이동시키기. 
            // 어떻게 이동시켜? 이동방향을 설정해줘서 FixedUpdate에서 물리이동 처리 
            isMoving = true;
            movementDirection = directionToTarget;
        }
    }

    private void FixedUpdate()
    {
        if(isMoving)
            Movement(movementDirection);
    }
    
    private void Movement(Vector3 direction)
    {
        direction = direction * moveSpeed; // 이동방향, 속도 적용

        // 이동
        _rigid.velocity = direction; // 이동 적용 
        _animator.SetInteger("State", 1); // 애니메이션 적용 
    }
}
