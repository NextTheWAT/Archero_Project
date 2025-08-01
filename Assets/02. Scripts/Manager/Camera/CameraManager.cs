using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [Header("제어할 카메라")]
    public Camera cameraToControl;

    [Header("플레이어 대상")]
    public Transform playerTransform;

    [Header("타이틀 상태일 때 카메라 위치")]
    public Transform titleCameraPosition;

    [Header("카메라 따라가는 속도")]
    public float followSpeed = 5f;

    [Header("플레이어 기준 오프셋")]
    public Vector3 followOffset = new Vector3(0f, 8f, 0f);

    private float fixedX;
    private float fixedY;

    private UIManager.GameState lastState;

    private void Start()
    {
        if (cameraToControl == null)
            cameraToControl = Camera.main;

        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        fixedY = cameraToControl.transform.position.y;
        lastState = UIManager.GameState.Main; // 초기 상태
    }

    private void LateUpdate()
    {
        if (UIManager.Instance == null || cameraToControl == null || playerTransform == null)
            return;

        var currentState = UIManager.Instance.GetCurrentState();

        // 상태가 Main → Playing으로 전환될 때 X값 고정
        if (currentState == UIManager.GameState.Playing && lastState != UIManager.GameState.Playing)
        {
            fixedX = playerTransform.position.x;
        }

        lastState = currentState; // 현재 상태 저장

        if (currentState == UIManager.GameState.Main)
        {
            cameraToControl.transform.position = Vector3.Lerp(
                cameraToControl.transform.position,
                titleCameraPosition.position,
                Time.deltaTime * followSpeed);
        }
        else if (currentState == UIManager.GameState.Playing || currentState == UIManager.GameState.Tutorial)
        {
            Vector3 targetPos = new Vector3(
                fixedX,
                fixedY + followOffset.y,
                playerTransform.position.z + followOffset.z);

            cameraToControl.transform.position = Vector3.Lerp(
                cameraToControl.transform.position,
                targetPos,
                Time.deltaTime * followSpeed);
        }
    }

    public void ResetFixedX(float x)
    {
        fixedX = x;
    }
}
