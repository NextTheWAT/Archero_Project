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
        direction = (targetTransform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            // 기본 LookRotation에 Y축으로 180도 추가
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180f, 0);
        }

        Destroy(gameObject, lifeTime);
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
            Debug.Log("플레이어에게 데미지");
            Destroy(gameObject); //투사체 파괴
        }
        else if (other.CompareTag("Enemy")) //몬스터 피격 처리
        {
            MonsterFSM monster = other.GetComponent<MonsterFSM>();
            if (monster != null)
            {
                monster.TakeDamage(damage); //데미지 주기
            }
            Destroy(gameObject); //투사체 파괴
        }
    }
}
