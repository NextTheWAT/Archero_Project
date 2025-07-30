using UnityEngine;

public class PlayerSpawnManager : Singleton<PlayerSpawnManager>
{
    [Header("스테이지별 시작 위치")]
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
            Debug.LogWarning("플레이어가 없습니다!");
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
            player.transform.rotation = targetSpawn.rotation; // 방향까지 맞춤
            Debug.Log($"[PlayerSpawnManager] 플레이어 위치 이동: {stageType}");
        }
    }
}
