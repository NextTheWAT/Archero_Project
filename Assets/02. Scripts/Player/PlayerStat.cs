using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


// 플레이어의 능력치와 상태(체력, 공격력 등) 관리
// 데미지 처리, 회복, 사망, 상태 초기화

public class PlayerStat : MonoBehaviour
{
    // 플레이어의 공격력
    public float attackPower = 10f;
    // 이동 속도
    public float moveSpeed = 5f;
    // 회전 속도
    public float rotationSpeed = 10f;
    // 공격 속도(공격 간 딜레이)
    public float attackSpeed = 1f;
    // 최대 체력
    public int maxHp = 100;
    // 현재 체력
    public int currentHp = 100;
    // 발사체 개수(멀티샷 등)
    public int projectileCount = 1;

    // 플레이어의 공격 사거리(기본값 20)
    public float attackRange = 20f;

    // 사망 여부 플래그
    private bool isDead = false;
    // 플레이어 및 자식 오브젝트의 Renderer 컴포넌트 배열
    private Renderer[] renderers;
    // 각 Renderer의 원래 색상 저장용 배열
    private Color[] originalColors;

    private void Start()
    {
        // 자식 포함 모든 Renderer 컴포넌트 가져오기
        renderers = GetComponentsInChildren<Renderer>();
        // 각 Renderer의 원래 색상 저장
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                originalColors[i] = renderers[i].material.color;
        }

        // 스킬 UI 매니저가 존재하면 UI 표시(현재 주석 처리)
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
        if (isDead) return; // 이미 사망 시 무시

        currentHp -= Mathf.RoundToInt(amount);
        SoundManager.Instance.Player_TakeDamage(); // 데미지 사운드 재생
        if (currentHp < 0) currentHp = 0;
        Debug.Log($"피해를 입음! 현재 체력: {currentHp}");

        StartCoroutine(FlashRed()); // 피격 시 붉게 점멸

        if (currentHp <= 0)
        {
            Die(); // 체력이 0 이하이면 사망 처리
        }
    }

    
    // 피격 시 잠시 붉은색으로 점멸하는 코루틴
    private System.Collections.IEnumerator FlashRed()
    {
        if (renderers == null || renderers.Length == 0)
            yield break;

        // 모든 Renderer를 붉은색으로 변경
        foreach (var r in renderers)
        {
            if (r.material.HasProperty("_Color"))
                r.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.2f);

        // 원래 색상으로 복구
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
                renderers[i].material.color = originalColors[i];
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("플레이어 사망!");

        // 이동 및 입력 비활성화
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        // 사망 애니메이션 트리거
        var animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetTrigger("Die");

        // 게임 오버 UI 표시
        UIManager.Instance.UpdateUI(UIManager.GameState.GameOver);
    }

   
    public void DebugDamage999()
    {
        Damage(999);
        Debug.Log("디버그: currentHp에 999 데미지 적용");
    }

    public void DebugDamage1()
    {
        Damage(1);
        Debug.Log("디버그: currentHp에 1 데미지 적용");
    }

 
    public bool IsDead => isDead;

  
    // 플레이어 상태 및 시각 효과를 초기화
    public void ResetStat()
    {
        isDead = false;                     
        currentHp = maxHp;

        // 이동/입력 활성화
        var movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = true;

        // 애니메이터 상태 초기화
        var animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
        }

        // 머티리얼 색상 원래대로 복구
        if (renderers != null && originalColors != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.HasProperty("_Color"))
                    renderers[i].material.color = originalColors[i];
            }
        }
    }


    // 에디터에서 플레이어의 공격 범위를 시각적으로 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
