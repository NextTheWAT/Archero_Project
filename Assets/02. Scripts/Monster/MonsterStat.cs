using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;

    public float attackDamage = 10f;
    public float attackDelay = 0.8f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 2f;

    private void Awake()
    {
        currentHp = maxHp;
    }
    public void TakeDamage(float damage)
    {
        SoundManager.Instance.Monster_SFX(1); // 몬스터 히트 사운드 재생
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
        }
    }
}
