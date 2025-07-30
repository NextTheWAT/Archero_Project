using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    private PlayerStat playerStat;


    private void Awake()
    {
        // 자동으로 태그가 "Player"인 오브젝트의 PlayerStat 컴포넌트 찾기
        if (playerStat == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                playerStat = obj.GetComponent<PlayerStat>();
        }
    }

    private void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        if (playerStat == null)
        {
            Debug.LogError("PlayerStat 스크립트를 가진 오브젝트를 찾을 수 없습니다!");
        }
    }

    public void ApplySkill(SkillData_ScriptableObject skillData)
    {
        if (playerStat == null) return;

        switch (skillData.type)
        {
            case SkillType.AttackPowerUp:
                playerStat.attackPower += skillData.value;
                break;
            case SkillType.MoveSpeedUp:
                playerStat.moveSpeed += skillData.value;
                break;
            case SkillType.AttackSpeedUp:
                playerStat.attackSpeed += skillData.value;
                break;
            case SkillType.HpRegen:
                playerStat.Heal(skillData.value);
                break;
            case SkillType.ProjectileCountUp:
                playerStat.projectileCount += Mathf.RoundToInt(skillData.value);
                break;
            default:
                Debug.LogWarning("알 수 없는 스킬 타입: " + skillData.type);
                break;
        }

        Debug.Log($"적용됨: {skillData.skillName}");
    }
}
