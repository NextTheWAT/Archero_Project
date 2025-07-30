using UnityEngine;

public class SkillUIManager : MonoBehaviour
{
    public static SkillUIManager Instance;

    public GameObject skillPanel;
    public UIAnimationHandler animationHandler;

    public Transform slotParent;
    public GameObject skillSlotPrefab;

    public SkillData_ScriptableObject[] allSkills;
    public int numberOfChoices = 3;

    public bool canSelect = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (animationHandler == null)
            animationHandler = skillPanel.GetComponent<UIAnimationHandler>();
    }

    public void ShowSkillUI()
    {
        if (animationHandler != null)
            animationHandler.Show();
        else
            skillPanel.SetActive(true);

        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        SkillData_ScriptableObject[] selected = GetRandomSkills(numberOfChoices);

        foreach (var skill in selected)
        {
            GameObject slot = Instantiate(skillSlotPrefab, slotParent);
            slot.GetComponent<SkillSlotUI>().SetSkill(skill);
        }

        canSelect = false;
        Invoke(nameof(EnableSelection), 0.4f);
    }

    void EnableSelection()
    {
        canSelect = true;
    }

    public void HideSkillUI()
    {
        if (animationHandler != null)
            animationHandler.Hide();
        else
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
