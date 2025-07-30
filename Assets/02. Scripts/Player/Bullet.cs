using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    public float damage = 10f; // �⺻ ������(PlayerStat���� �Ҵ� �޾ƿ� �� ����)

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
            // MonsterStat ������Ʈ ã��
            MonsterStat stat = other.GetComponent<MonsterStat>();
            if (stat != null)
            {
                stat.currentHp -= damage;
                if (stat.currentHp <= 0)
                {
                    stat.currentHp = 0;
                    // �ʿ�� ���� ��� ó�� �߰�
                    // Destroy(other.gameObject); // ����
                }
            }
            Destroy(gameObject);
        }
    }
}
