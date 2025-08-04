using UnityEngine;

public class StageChangeTrigger : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // �� Ʈ���Ű� �̵��� �������� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.Instance.isCleared == true)
        {
            GameManager.Instance.SetStage(targetStage);
            Debug.Log($"�������� ����: {targetStage}");

            if (GameManager.Instance.CurrentStage != StageType.Tutorial &&
                GameManager.Instance.CurrentStage != StageType.MainStage &&
                GameManager.Instance.CurrentStage != StageType.End)
            {
                SkillUIManager.Instance.ShowSkillUI();
            }
        }
    }



}
