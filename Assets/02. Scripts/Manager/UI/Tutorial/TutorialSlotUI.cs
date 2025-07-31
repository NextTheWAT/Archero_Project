using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialSlotUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI infoText;
    //public Image background;
    private SkillData_ScriptableObject skillData;

    public void SetSkill(SkillData_ScriptableObject data)
    {
        skillData = data;
        iconImage.sprite = data.icon;
        nameText.text = data.skillName;
        infoText.text = data.skillInfo;
    }
}
