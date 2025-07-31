using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public float speed = 10f; //투사체 이동속도
    public float damage = 10f; //데미지량
    public float lifeTime = 3f; //사라지는 시간
    private Transform target; //타겟

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        Destroy(gameObject, lifeTime); //일정 시간 지나면 투사체 자동 파괴
    }

    private void Update()
    {
        if (target == null) return; //타켓 없으면 행동 안함
        
        //타겟방향 구해서 정규화
        Vector3 direction = (target.position - transform.position).normalized;

        //투사체 해당 방향으로 이동
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
        //충돌했을때 호출
    {
        if (other.CompareTag("Player")) //충돌 대상이 Player
        {
            Debug.Log("데미지 입힘");

            Destroy(gameObject); //투사체 파
        }
    }
}
