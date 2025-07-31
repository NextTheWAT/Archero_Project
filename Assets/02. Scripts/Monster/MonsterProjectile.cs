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
        //���� �̸� ����ϰ� ����
        direction = (targetTransform.position - transform.position).normalized;
        direction.y = 0;

        if(direction != Vector3.zero) //����ü ȸ�� ����
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        Destroy(gameObject, lifeTime); //���� �ð� ������ ����ü �ڵ� �ı�
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
            Debug.Log("������ ����");
            Destroy(gameObject); //����ü �ı�
        }
    }
}
