using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("������ ī�޶�")]
    public Camera cameraToControl; // Main Camera ���

    [Header("�÷��̾� ���")]
    public Transform playerTransform; // �÷��̾� Transform

    [Header("Ÿ��Ʋ ������ �� ī�޶� ��ġ")]
    public Transform titleCameraPosition;

    [Header("ī�޶� ���󰡴� �ӵ�")]
    public float followSpeed = 5f;

    [Header("�÷��̾� ���� ������")]
    public Vector3 followOffset = new Vector3(0f, 8f, 0f); // Y ������ �⺻��

    private float fixedY; // Y ������ ����

    private void Start()
    {
        // ī�޶� �ڵ� ���
        if (cameraToControl == null)
        {
            cameraToControl = Camera.main;
        }

        // �÷��̾� �ڵ� �˻�
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        // Y�� ����
        fixedY = cameraToControl.transform.position.y;
    }

    private void LateUpdate()
    {
        if (UIManager.Instance == null || cameraToControl == null)
            return;

        var currentState = UIManager.Instance.GetCurrentState();

        if (currentState == UIManager.GameState.Main)
        {
            // Ÿ��Ʋ ����: ���� ��ġ�� �̵�
            cameraToControl.transform.position = Vector3.Lerp(
                cameraToControl.transform.position,
                titleCameraPosition.position,
                Time.deltaTime * followSpeed);
        }
        else if (currentState == UIManager.GameState.Playing)
        {
            if (playerTransform == null) return;

            // �÷��̾� �������� X, Z�� ���󰡰� Y�� ���� + ������
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
