using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TimeController : MonoBehaviour
{
    private float originalTimeScale = 1f; // 원래 시간 속도 저장

    [SerializeField] TMP_Text currentTimeSpeed;
 
    private void Start()
    {
        Time.timeScale = originalTimeScale;
        currentTimeSpeed.text = $"X 2";
    }

    // 시간 속도를 2배로 증가시키는 함수
    public void SpeedUpTime()
    {
        if (Time.timeScale >= 4f)
        {
            Time.timeScale = 4f;
            return;
        }
        else
        {
            Time.timeScale *= 2f;
            Debug.Log($"시간 속도 증가: {Time.timeScale}");
        }
        currentTimeSpeed.text= $"X {Time.timeScale}";
    }

    // 시간 속도를 원래대로 복원하는 함수
    public void ResetTime()
    {
        Time.timeScale = originalTimeScale;
        Debug.Log($"시간 속도 복원: {Time.timeScale}");

        currentTimeSpeed.text = $"X 2";
    }
}
