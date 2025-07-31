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
            // 이전 슬롯 제거
            foreach (Transform child in slotParent)
                Destroy(child.gameObject);

            // 현재 스킬 슬롯 생성
            SkillData_ScriptableObject skill = tutorialSkills[currentIndex];
            GameObject slot = Instantiate(skillSlotPrefab, slotParent);
            slot.GetComponent<SkillSlotUI>().SetSkill(skill);

            animationHandler.Show();

            currentIndex++;
            yield return new WaitForSeconds(displayDuration);
            animationHandler.Hide();
            yield return new WaitForSeconds(0.5f); // 애니메이션 후 잠시 대기
        }

        // 튜토리얼 끝
        EndTutorial();
    }

    private void EndTutorial()
    {
        Debug.Log("스킬 튜토리얼 완료!");
        gameObject.SetActive(false);
        // 필요하면 GameManager.Instance.SetStage(...) 호출
    }
}
