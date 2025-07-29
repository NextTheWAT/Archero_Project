using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 움직임 제어 클래스 
public class BossMovementController : MonoBehaviour
{
    /*
     
    구현 기능 : 보스가 플레이어 따라가기 

    필요한 것들 
    - 플레이어 따라가기
        - 보스가 플레이어 좌표를 계속 체크한다. 
        - 플레이어 보스가 따라갈 수 있는 거리(distance)로 들어오면 보스가 플레이어 위치에서 살짝 떨어진 거리까지 이동한다 (서로 붙는 정도)
        - 
    - 플레이어 좌표 (O)
    - 보스 이동속도 (O)
    - 보스 이동 애니메이션 적용 (중요X)

    구현 방법
    - 플레이어 좌표를 받아온다
    - 보스가 플레이어를 인식해서 따라갈 거리를 정해둔다.
    - 보스와 플레이어의 거리를 계산한다. 
    - 보스가 플레이어를 바라보는 방향을 계산한다. 
    - 계산한 거리와 보스의 거리를 비교해서 거리 내로 들어오면 보스를 이동시킨다. 
     
     */
    public Transform player; // 플레이어 위치 

    public float moveSpeed; // 보스 이동속도 
    public float distance; // 보스 플레이어를 따라갈 수 있는 거리 (내가 정하는 것)
    public Vector3 directionToTarget; // 보스가 플레이어를 바라보는 방향

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        directionToTarget = MathF.Abs()
    }
}
