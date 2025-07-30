using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CanvasCameraSetter : MonoBehaviour
{
    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();

        // Render Mode�� Screen Space - Camera�� ���� ó��
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && canvas.worldCamera == null)
        {
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                canvas.worldCamera = mainCam;
                Debug.Log($"[CanvasCameraSetter] {canvas.name}�� RenderCamera�� �ڵ����� MainCamera�� �����߽��ϴ�.");
            }
            else
            {
                Debug.LogWarning("[CanvasCameraSetter] ���� ī�޶� ã�� ���߽��ϴ�. Tag�� 'MainCamera'���� Ȯ���ϼ���.");
            }
        }
    }
}
