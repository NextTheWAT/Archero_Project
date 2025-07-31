using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public float damage = 10f; // 기본 데미지(PlayerStat에서 할당 받아올 수 있음)

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // MonsterStat 컴포넌트 찾기
            MonsterStat stat = other.GetComponent<MonsterStat>();
            if (stat != null)
            {
                stat.currentHp -= damage;
                if (stat.currentHp <= 0)
                {
                    stat.currentHp = 0;
                    // 필요시 몬스터 사망 처리 추가
                    // Destroy(other.gameObject); // 예시
                }
            }
            Destroy(gameObject);
        }
    }
}
