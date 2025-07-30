using UnityEngine;

public class StageChangeTrigger : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // 이 트리거가 이동할 스테이지 설정

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어만 통과할 수 있도록
        {
            GameManager.Instance.SetStage(targetStage);
            Debug.Log($"스테이지 변경: {targetStage}");
        }
    }


}
