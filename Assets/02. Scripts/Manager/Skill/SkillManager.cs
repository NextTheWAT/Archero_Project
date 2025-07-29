using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    private PlayerStat playerStat;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        if (playerStat == null)
        {
            Debug.LogError("PlayerStat ��ũ��Ʈ�� ���� ������Ʈ�� ã�� �� �����ϴ�!");
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
                Debug.LogWarning("�� �� ���� ��ų Ÿ��: " + skillData.type);
                break;
        }

        Debug.Log($"�����: {skillData.skillName}");
    }
}
