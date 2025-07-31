using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public float attackPower = 10f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float attackSpeed = 1f;
    public int maxHp = 100;
    public int currentHp = 100;
    public int projectileCount = 1;

    private bool isDead = false;

    private void Start()
    {
        if (SkillUIManager.Instance != null)
        {
            //SkillUIManager.Instance.ShowSkillUI();
        }
        else
        {
            Debug.LogWarning("SkillUIManager.Instance is null. ShowSkillUI() 호출 실패");
        }
    }

    public void Heal(float amount)
    {
        currentHp += Mathf.RoundToInt(amount);
        if (currentHp > maxHp) currentHp = maxHp;
        Debug.Log($"체력 회복됨! 현재 체력: {currentHp}");
    }

    public void Damage(float amount)
    {
        if (isDead) return;

        currentHp -= Mathf.RoundToInt(amount);
        if (currentHp < 0) currentHp = 0;
        Debug.Log($"피해를 입음! 현재 체력: {currentHp}");

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("플레이어 사망!");

        // 예시: 이동/입력/애니메이션 비활성화
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Die");

        // 필요시 UIManager 등에서 게임오버 처리 호출
        // UIManager.Instance.ShowGameOver();
    }

    // 디버그: currentHp에서 999만큼 데미지를 주는 메서드
    public void DebugDamage999()
    {
        Damage(999);
        Debug.Log("디버그: currentHp에 999 데미지 적용");
    }

    public bool IsDead => isDead;
}
