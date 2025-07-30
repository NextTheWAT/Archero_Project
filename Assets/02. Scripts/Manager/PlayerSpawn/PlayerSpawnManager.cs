using UnityEngine;

public class PlayerSpawnManager : Singleton<PlayerSpawnManager>
{
    [Header("���������� ���� ��ġ")]
    public Transform mainSpawnPoint;
    public Transform stage1SpawnPoint;
    public Transform stage2SpawnPoint;
    public Transform stage3SpawnPoint;
    public Transform stage4SpawnPoint;
    public Transform bossSpawnPoint;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void MovePlayerToStage(StageType stageType)
    {
        if (player == null)
        {
            Debug.LogWarning("�÷��̾ �����ϴ�!");
            return;
        }

        Transform targetSpawn = null;

        switch (stageType)
        {
            case StageType.MainStage:
                targetSpawn = mainSpawnPoint;
                break;
            case StageType.Stage1:
                targetSpawn = stage1SpawnPoint;
                break;
            case StageType.Stage2:
                targetSpawn = stage2SpawnPoint;
                break;
            case StageType.Stage3:
                targetSpawn = stage3SpawnPoint;
                break;
            case StageType.Stage4:
                targetSpawn = stage4SpawnPoint;
                break;
            case StageType.Boss:
                targetSpawn = bossSpawnPoint;
                break;
        }

        if (targetSpawn != null)
        {
            player.transform.position = targetSpawn.position;
            player.transform.rotation = targetSpawn.rotation; // ������� ����
            Debug.Log($"[PlayerSpawnManager] �÷��̾� ��ġ �̵�: {stageType}");
        }
    }
}
