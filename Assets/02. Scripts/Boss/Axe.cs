using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Axe : MonoBehaviour
{
    // ���� �ɷ�ġ
    public int attackPower;
    public int specialAttackPower;
    

    public PlayerManager playerManager;
    public PlayerStat playerStat;

    public Animator animator;

    public BossManager bossManager;

    public BossController bossController;

    private bool isDamagedDelay = false; // �÷��̾ �������� �Ծ����� ����

    public bool isSpecialDamage;
    public bool isNormalDamage;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerStat = playerManager.playerStat;

        bossManager = BossManager.Instance;

        animator = bossManager.bossObj.GetComponent<Animator>();

        bossController = bossManager.bossMovementController;
    }

    public void Attack()
    {
        animator.SetInteger("State", (int)ActionState.Attack);
        isNormalDamage = true;
        //Debug.Log("�Ϲݰ���"); // �Ϲݰ���
                           //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                           // ������ ������ ������ ó�� 

        // �ڷ�ƾ -> �����̶� ���, Invoke
        // yield return new Wait...(2)
        //isNormalAttack = true; // ������ �ð� �ο� 
                               // �ִϸ��̼� ����
    }

    public void SpecialAttack()
    {
        animator.SetInteger("State", (int)ActionState.SpecialAttack);
        isSpecialDamage = true;
        //Debug.Log("Ư������"); // ������, �ָ� ������ ���� ������? 
                           //TakeDamage(); // ������ ������ �޼��� ȣ�� 
                           // ������ ������ ������ ó�� 
        //isSpecialAttack = true; // ������ �ð� �ο� 
                                // �ִϸ��̼� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // ������ �÷��̾�� ������ 
        {
            if (isDamagedDelay == false && bossController.isNormalAttack == false && isNormalDamage)
            {
                isDamagedDelay = true; // �÷��̾ �������� �Ծ��ٰ� ����
                isNormalDamage = false;
                Debug.Log("������ ó��");
                Debug.Log("�Ϲݰ���");
                StartCoroutine(DelayisDamage()); // 1.5�� �Ŀ� �ٽ� �������� ���� �� �ֵ��� ���� => �ִϸ��̼��� ���� 1.5�ʰ� �������� 
                playerStat.Damage(attackPower);
            }
            //Debug.Log($"(������) �÷��̾� HP = {playerStat.currentHp}");
            //playerStat.currentHp -= attackPower;
            //Debug.Log($"(������) �÷��̾� HP = {playerStat.currentHp}");
            else if(isDamagedDelay == false && bossController.isSpecialAttack == false && isSpecialDamage)
            {
                isDamagedDelay = true;
                isSpecialDamage = false;
                Debug.Log("������ ó��");
                Debug.Log("Ư������");
                StartCoroutine(DelayisSpecialDamage());
                playerStat.Damage(specialAttackPower);
            }
        }
    }

    IEnumerator DelayisDamage()
    {
        yield return new WaitForSeconds(1.5f);
        isDamagedDelay = false; // 1.5�� �Ŀ� �ٽ� �������� ���� �� �ֵ��� ����
    }

    IEnumerator DelayisSpecialDamage()
    {
        yield return new WaitForSeconds(2.5f);
        isDamagedDelay = false; // 3�� �Ŀ� �ٽ� �������� ���� �� �ֵ��� ����
    }
}
