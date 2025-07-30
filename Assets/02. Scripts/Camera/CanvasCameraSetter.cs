using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasCameraSetter : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();

        // Render Mode가 Screen Space - Camera일 때만 처리
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                canvas.worldCamera = mainCam;
                Debug.Log($"[CanvasCameraSetter] {canvas.name}의 RenderCamera를 자동으로 MainCamera로 설정했습니다.");
            }
            else
            {
                Debug.LogWarning("[CanvasCameraSetter] 메인 카메라를 찾지 못했습니다. Tag가 'MainCamera'인지 확인하세요.");
            }
        }
    }
}
