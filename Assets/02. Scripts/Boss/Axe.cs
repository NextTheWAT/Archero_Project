using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Axe : MonoBehaviour
{
    // 도끼 능력치
    public int attackPower;
    public int specialAttackPower;
    

    public PlayerManager playerManager;
    public PlayerStat playerStat;

    public Animator animator;

    public BossManager bossManager;

    public BossController bossController;

    private bool isDamagedDelay = false; // 플레이어가 데미지를 입었는지 여부

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
        //Debug.Log("일반공격"); // 일반공격
                           //TakeDamage(); // 데미지 입히는 메서드 호출 
                           // 도끼가 닿으면 데미지 처리 

        // 코루틴 -> 딜레이때 사용, Invoke
        // yield return new Wait...(2)
        //isNormalAttack = true; // 딜레이 시간 부여 
                               // 애니메이션 적용
    }

    public void SpecialAttack()
    {
        animator.SetInteger("State", (int)ActionState.SpecialAttack);
        isSpecialDamage = true;
        //Debug.Log("특수공격"); // 강공격, 멀리 있으면 도끼 던지기? 
                           //TakeDamage(); // 데미지 입히는 메서드 호출 
                           // 도끼가 닿으면 데미지 처리 
        //isSpecialAttack = true; // 딜레이 시간 부여 
                                // 애니메이션 적용
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // 도끼가 플레이어와 닿으면 
        {
            if (isDamagedDelay == false && bossController.isNormalAttack == false && isNormalDamage)
            {
                isDamagedDelay = true; // 플레이어가 데미지를 입었다고 설정
                isNormalDamage = false;
                Debug.Log("데미지 처리");
                Debug.Log("일반공격");
                StartCoroutine(DelayisDamage()); // 1.5초 후에 다시 데미지를 입힐 수 있도록 설정 => 애니메이션을 보고 1.5초가 적당했음 
                playerStat.Damage(attackPower);
            }
            //Debug.Log($"(변경전) 플레이어 HP = {playerStat.currentHp}");
            //playerStat.currentHp -= attackPower;
            //Debug.Log($"(변경후) 플레이어 HP = {playerStat.currentHp}");
            else if(isDamagedDelay == false && bossController.isSpecialAttack == false && isSpecialDamage)
            {
                isDamagedDelay = true;
                isSpecialDamage = false;
                Debug.Log("데미지 처리");
                Debug.Log("특수공격");
                StartCoroutine(DelayisSpecialDamage());
                playerStat.Damage(specialAttackPower);
            }
        }
    }

    IEnumerator DelayisDamage()
    {
        yield return new WaitForSeconds(1.5f);
        isDamagedDelay = false; // 1.5초 후에 다시 데미지를 입힐 수 있도록 설정
    }

    IEnumerator DelayisSpecialDamage()
    {
        yield return new WaitForSeconds(2.5f);
        isDamagedDelay = false; // 3초 후에 다시 데미지를 입힐 수 있도록 설정
    }
}
