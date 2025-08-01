using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public float speed = 10f; //����ü �̵��ӵ�
    public float damage = 10f; //��������
    public float lifeTime = 3f; //������� �ð�

    private Vector3 direction; //�ѹ��� ����� ����

    public void SetTarget(Transform targetTransform)
    {
        direction = (targetTransform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            // �⺻ LookRotation�� Y������ 180�� �߰�
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180f, 0);
        }

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //����ü �ش� �������� �̵�
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    //�浹������ ȣ��
    {
        if (other.CompareTag("Player")) //�浹 ����� Player
        {
            Debug.Log("�÷��̾�� ������");
            Destroy(gameObject); //����ü �ı�
        }
        else if (other.CompareTag("Enemy")) //���� �ǰ� ó��
        {
            MonsterFSM monster = other.GetComponent<MonsterFSM>();
            if (monster != null)
            {
                monster.TakeDamage(damage); //������ �ֱ�
            }
            Destroy(gameObject); //����ü �ı�
        }
    }
}
