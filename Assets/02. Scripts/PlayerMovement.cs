using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        // �̵�
        if (moveDir.magnitude > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            animator.SetBool("isMoving", true);

            // �̵���������ȸ��
            Quaternion targetRot = Quaternion.LookRotation(-moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);

            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                Vector3 dirToEnemy = (nearestEnemy.transform.position - transform.position);
                dirToEnemy.y = 0f;
                if (dirToEnemy.sqrMagnitude > 0.01f)
                {
                    // ����������ȸ��
                    Quaternion targetRot = Quaternion.LookRotation(-dirToEnemy, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    // ���� ����� ���� ã�� �Լ� (Enemy �±� ���) <-- �±׹ݵ�ü���
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDist = float.MaxValue;
        Vector3 currentPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float dist = (enemy.transform.position - currentPos).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }
}
