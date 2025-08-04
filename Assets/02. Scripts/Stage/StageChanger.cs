using UnityEngine;

public class StageChangeTrigger : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // 이 트리거가 이동할 스테이지 설정

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.isCleared == true)
        {
            GameManager.Instance.SetStage(targetStage);
            Debug.Log($"스테이지 변경: {targetStage}");

            if (GameManager.Instance.CurrentStage != StageType.Tutorial &&
                GameManager.Instance.CurrentStage != StageType.MainStage &&
                GameManager.Instance.CurrentStage != StageType.End)
            {
                SkillUIManager.Instance.ShowSkillUI();
            }
        }
    }



}
