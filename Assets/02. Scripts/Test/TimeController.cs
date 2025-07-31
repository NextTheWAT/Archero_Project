using UnityEngine;

public class TimeController : MonoBehaviour
{
    private float originalTimeScale = 1f; // 원래 시간 속도 저장
    private float TimeScale;

    private void Start()
    {
        TimeScale = originalTimeScale;
    }

    // 시간 속도를 2배로 증가시키는 함수
    public void SpeedUpTime()
    {
        TimeScale = Time.timeScale; // 현재 속도 저장
        Time.timeScale = TimeScale * 2f;
        Debug.Log($"시간 속도 증가: {Time.timeScale}");
    }

    // 시간 속도를 원래대로 복원하는 함수
    public void ResetTime()
    {
        Time.timeScale = originalTimeScale;
        Debug.Log($"시간 속도 복원: {Time.timeScale}");
    }
    public void DownSetTime()
    {
        Time.timeScale = 0.25f;
        Debug.Log($"시간 속도 복원: {Time.timeScale}");
    }
}
