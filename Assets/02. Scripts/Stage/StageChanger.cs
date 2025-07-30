using UnityEngine;

public class StageChangeTrigger : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // �� Ʈ���Ű� �̵��� �������� ����

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �÷��̾ ����� �� �ֵ���
        {
            GameManager.Instance.SetStage(targetStage);
            Debug.Log($"�������� ����: {targetStage}");
        }
    }


}
