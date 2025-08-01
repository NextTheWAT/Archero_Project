using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Damage : MonoBehaviour
{
    public PlayerStat playerStat;

    private bool isDamagedDelay = false;

    public MonsterFSM monsterFSM; // MonsterFSM �ν��Ͻ�

    private void Start()
    {
        // PlayerManager �ν��Ͻ����� playerStat�� �����ɴϴ�.
        playerStat = GameObject.FindObjectOfType<PlayerStat>();
        monsterFSM = GetComponentInParent<MonsterFSM>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDamagedDelay && monsterFSM.isAnimDamage == false)
        {
            isDamagedDelay = true;
            playerStat.Damage(10f);
            Debug.Log("�÷��̾ ���� ������ �޾ҽ��ϴ�.");
            StartCoroutine(DelayisDamage()); // 1.5�� �Ŀ� �ٽ� �������� ���� �� �ֵ��� ����
        }
    }

    IEnumerator DelayisDamage()
    {
        yield return new WaitForSeconds(1.5f);
        isDamagedDelay = false; // 1.5�� �Ŀ� �ٽ� �������� ���� �� �ֵ��� ����
    }


}
