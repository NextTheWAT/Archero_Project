using UnityEngine;

[CreateAssetMenu(fileName = "SkillData_ScriptableObject", menuName = "Skill/SkillData")]
public class SkillData_ScriptableObject : ScriptableObject
{
    public string skillName;
    public string skillInfo;
    public Sprite icon;
    public Color backgroundColor;

    // 강화 수치 (예: 공격력 증가량, 이동속도 등)
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
