using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    // ���� �ɷ�ġ
    public int attackPower;

    public PlayerManager playerManager;
    public PlayerStat playerStat;

    public Animator animator;

    public BossManager bossManager;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerStat = playerManager.playerStat;

        animator = bossManager.bossObj.GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetInteger("State", (int)ActionState.Attack);
        Debug.Log("�Ϲݰ���"); // �Ϲݰ���
                           //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                           // ������ ������ ������ ó�� 

        // �ڷ�ƾ -> �����̶� ���, Invoke
        // yield return new Wait...(2)
        isNormalAttack = true; // ������ �ð� �ο� 
                               // �ִϸ��̼� ����
    }

    public void SpecialAttack()
    {
        animator.SetInteger("State", (int)ActionState.SpecialAttack);
        Debug.Log("Ư������"); // ������, �ָ� ������ ���� ������? 
                           //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                           // ������ ������ ������ ó�� 
        isSpecialAttack = true; // ������ �ð� �ο� 
                                // �ִϸ��̼� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // ������ �÷��̾�� ������ 
        {
            Debug.Log("������ ó��");
            Debug.Log($"(������) �÷��̾� HP = {playerStat.currentHp}");
            playerStat.currentHp -= attackPower;
            Debug.Log($"(������) �÷��̾� HP = {playerStat.currentHp}");
        }
    }
}
