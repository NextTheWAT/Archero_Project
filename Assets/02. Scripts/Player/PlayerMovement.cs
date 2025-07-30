using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private PlayerStat playerStat; // PlayerStat 참조

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerStat = GetComponent<PlayerStat>(); // PlayerStat 할당
    }

    void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        rb.MovePosition(rb.position + moveDir * (playerStat.moveSpeed * Time.fixedDeltaTime));
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        // 이동
        if (moveDir.magnitude > 0)
        {
            transform.position += moveDir * playerStat.moveSpeed * Time.deltaTime;
            animator.SetBool("isMoving", true);

            // 이동방향으로회전
            Quaternion targetRot = Quaternion.LookRotation(-moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, playerStat.rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);

            GameObject nearestEnemy = EnemyUtil.FindNearestEnemy(transform.position);
            if (nearestEnemy != null)
            {
                Vector3 dirToEnemy = (nearestEnemy.transform.position - transform.position);
                dirToEnemy.y = 0f;
                if (dirToEnemy.sqrMagnitude > 0.01f)
                {
                    // 적방향으로회전
                    Quaternion targetRot = Quaternion.LookRotation(-dirToEnemy, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, playerStat.rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
