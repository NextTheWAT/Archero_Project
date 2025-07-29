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
    - 플레이어 좌표
    - 보스 이동속도
    - 보스 이동 애니메이션 적용 (중요X)

    구현 방법
    - 플레이어 좌표를 받아온다
    - 보스가 플레이어를 인식해서 따라갈 사정거리를 정해둔다.
    - 보스와 플레이어의 거리를 계산한다. 
    - 보스가 플레이어를 바라보는 방향을 계산한다. 
    - 계산한 거리와 보스의 사정거리를 비교해서 사정거리 내로 들어오면 보스를 이동시킨다. 
     
     */
    public Transform player;

}
