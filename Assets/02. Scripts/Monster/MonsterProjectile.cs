using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public float speed = 10f; //����ü �̵��ӵ�
    public float damage = 10f; //��������
    public float lifeTime = 3f; //������� �ð�
    private Transform target; //Ÿ��

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        Destroy(gameObject, lifeTime); //���� �ð� ������ ����ü �ڵ� �ı�
    }

    private void Update()
    {
        if (target == null) return; //Ÿ�� ������ �ൿ ����
        
        //Ÿ�ٹ��� ���ؼ� ����ȭ
        Vector3 direction = (target.position - transform.position).normalized;

        //����ü �ش� �������� �̵�
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
        //�浹������ ȣ��
    {
        if (other.CompareTag("Player")) //�浹 ����� Player
        {
            Debug.Log("������ ����");

            Destroy(gameObject); //����ü ��
        }
    }
}
