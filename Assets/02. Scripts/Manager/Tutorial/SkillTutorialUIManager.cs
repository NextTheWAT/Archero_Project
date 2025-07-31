using System.Collections;
using UnityEngine;

public class SkillTutorialUIManager : Singleton<SkillTutorialUIManager>
{
    public GameObject tutorialPanel;
    public UIAnimationHandler animationHandler;

    public Transform slotParent;
    public GameObject skillSlotPrefab;

    public SkillData_ScriptableObject[] tutorialSkills;
    public float displayDuration = 3f;

    private int currentIndex = 0;
    private Coroutine tutorialCoroutine;

    private void Awake()
    {
        if (animationHandler == null)
            animationHandler = tutorialPanel.GetComponent<UIAnimationHandler>();
    }

    public void StartTutorial()
    {
        currentIndex = 0;

        if (tutorialCoroutine != null)
            StopCoroutine(tutorialCoroutine);

        tutorialCoroutine = StartCoroutine(ShowSkillsSequentially());
    }

    private IEnumerator ShowSkillsSequentially()
    {
        while (currentIndex < tutorialSkills.Length)
        {
            // ���� ���� ����
            foreach (Transform child in slotParent)
                Destroy(child.gameObject);

            // ���� ��ų ���� ����
            SkillData_ScriptableObject skill = tutorialSkills[currentIndex];
            GameObject slot = Instantiate(skillSlotPrefab, slotParent);
            slot.GetComponent<SkillSlotUI>().SetSkill(skill);

            animationHandler.Show();

            currentIndex++;
            yield return new WaitForSeconds(displayDuration);
            animationHandler.Hide();
            yield return new WaitForSeconds(0.5f); // �ִϸ��̼� �� ��� ���
        }

        // Ʃ�丮�� ��
        EndTutorial();
    }

    private void EndTutorial()
    {
        Debug.Log("��ų Ʃ�丮�� �Ϸ�!");
        gameObject.SetActive(false);
        // �ʿ��ϸ� GameManager.Instance.SetStage(...) ȣ��
    }
}
