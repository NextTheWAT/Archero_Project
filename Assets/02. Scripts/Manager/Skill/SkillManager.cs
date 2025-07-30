using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    private PlayerStat playerStat;


    private void Awake()
    {
        // �ڵ����� �±װ� "Player"�� ������Ʈ�� PlayerStat ������Ʈ ã��
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
