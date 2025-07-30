using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("제어할 카메라")]
    public Camera cameraToControl; // Main Camera 등록

    [Header("플레이어 대상")]
    public Transform playerTransform; // 플레이어 Transform

    [Header("타이틀 상태일 때 카메라 위치")]
    public Transform titleCameraPosition;

    [Header("카메라 따라가는 속도")]
    public float followSpeed = 5f;

    [Header("플레이어 기준 오프셋")]
    public Vector3 followOffset = new Vector3(0f, 8f, 0f); // Y 오프셋 기본값

    private float fixedY; // Y 고정값 저장

    private void Start()
    {
        // 카메라 자동 등록
        if (cameraToControl == null)
        {
            cameraToControl = Camera.main;
        }

        // 플레이어 자동 검색
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        // Y값 고정
        fixedY = cameraToControl.transform.position.y;
    }

    private void LateUpdate()
    {
        if (UIManager.Instance == null || cameraToControl == null)
            return;

        var currentState = UIManager.Instance.GetCurrentState();

        if (currentState == UIManager.GameState.Main)
        {
            // 타이틀 상태: 고정 위치로 이동
            cameraToControl.transform.position = Vector3.Lerp(
                cameraToControl.transform.position,
                titleCameraPosition.position,
                Time.deltaTime * followSpeed);
        }
        else if (currentState == UIManager.GameState.Playing)
        {
            if (playerTransform == null) return;

            // 플레이어 기준으로 X, Z는 따라가고 Y는 고정 + 오프셋
            Vector3 targetPos = new Vector3(
                playerTransform.position.x + followOffset.x,
                fixedY + followOffset.y,
                playerTransform.position.z + followOffset.z);

            cameraToControl.transform.position = Vector3.Lerp(
                cameraToControl.transform.position,
                targetPos,
                Time.deltaTime * followSpeed);
        }
    }
}
