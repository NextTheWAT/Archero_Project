using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public float speed = 10f; //투사체 이동속도
    public float damage = 10f; //데미지량
    public float lifeTime = 3f; //사라지는 시간

    private Vector3 direction; //한번만 계산할 방향

    public void SetTarget(Transform targetTransform)
    {
        //방향 미리 계산하고 저장
        direction = (targetTransform.position - transform.position).normalized;
        direction.y = 0;

        if(direction != Vector3.zero) //투사체 회전 설정
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        Destroy(gameObject, lifeTime); //일정 시간 지나면 투사체 자동 파괴
    }

    private void Update()
    {
        //투사체 해당 방향으로 이동
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    //충돌했을때 호출
    {
        if (other.CompareTag("Player")) //충돌 대상이 Player
        {
            Debug.Log("데미지 입힘");
            Destroy(gameObject); //투사체 파괴
        }
    }
}
