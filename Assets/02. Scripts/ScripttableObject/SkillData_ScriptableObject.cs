using UnityEngine;

[CreateAssetMenu(fileName = "SkillData_ScriptableObject", menuName = "Skill/SkillData")]
public class SkillData_ScriptableObject : ScriptableObject
{
    public string skillName;
    public string skillInfo;
    public Sprite icon;
    public Color backgroundColor;

    // ��ȭ ��ġ (��: ���ݷ� ������, �̵��ӵ� ��)
    public SkillType type;
    public float value;
}

public enum SkillType
{
    AttackPowerUp,
    MoveSpeedUp,
    AttackSpeedUp,
    HpRegen,
    ProjectileCountUp
}
