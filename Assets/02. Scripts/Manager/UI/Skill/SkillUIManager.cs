// SkillUIManager.cs ÆÄÀÏ
using UnityEngine;

public class SkillUIManager : MonoBehaviour
{
    public static SkillUIManager Instance;

    public GameObject skillPanel;
    public Transform slotParent;
    public GameObject skillSlotPrefab;

    public SkillData_ScriptableObject[] allSkills;
    public int numberOfChoices = 3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public void ShowSkillUI()
    {
        skillPanel.SetActive(true);

        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        SkillData_ScriptableObject[] selected = GetRandomSkills(numberOfChoices);

        foreach (var skill in selected)
        {
            GameObject slot = Instantiate(skillSlotPrefab, slotParent);
            slot.GetComponent<SkillSlotUI>().SetSkill(skill);
        }
    }

    public void HideSkillUI()
    {
        skillPanel.SetActive(false);
    }

    SkillData_ScriptableObject[] GetRandomSkills(int count)
    {
        SkillData_ScriptableObject[] result = new SkillData_ScriptableObject[count];
        var list = new System.Collections.Generic.List<SkillData_ScriptableObject>(allSkills);

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, list.Count);
            result[i] = list[index];
            list.RemoveAt(index);
        }

        return result;
    }
}
