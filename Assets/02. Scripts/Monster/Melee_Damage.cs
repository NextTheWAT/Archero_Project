using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Damage : MonoBehaviour
{
    public PlayerStat playerStat;

    private bool isDamagedDelay = false;

    public MonsterFSM monsterFSM; // MonsterFSM 인스턴스

    private void Start()
    {
        // PlayerManager 인스턴스에서 playerStat을 가져옵니다.
        playerStat = GameObject.FindObjectOfType<PlayerStat>();
        monsterFSM = GetComponentInParent<MonsterFSM>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 적이 죽었으면 데미지 처리하지 않음
        if (monsterFSM.isDie) return;

        if (other.CompareTag("Player") && !isDamagedDelay && monsterFSM.isAnimDamage == false)
        {
            isDamagedDelay = true;
            playerStat.Damage(10f);
            Debug.Log("플레이어가 근접 공격을 받았습니다.");
            StartCoroutine(DelayisDamage());
        }
    }

    IEnumerator DelayisDamage()
    {
        yield return new WaitForSeconds(1.5f);
        isDamagedDelay = false; // 1.5초 후에 다시 데미지를 입힐 수 있도록 설정
    }


}
