using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("스폰 위치들 (빈 오브젝트들)")]
    public Transform[] spawnPoints;

    [Header("몬스터 프리팹")]
    public GameObject monsterPrefab;

    [Header("소환할 몬스터 수")]
    public int numberOfMonsters = 5;

    [Header("시작 시 자동 소환 여부")]
    public bool spawnOnStart = true;

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnMonsters();
        }
    }

    public void SpawnMonsters()
    {
        if (spawnPoints.Length == 0 || monsterPrefab == null)
        {
            Debug.LogWarning("스폰 위치 또는 프리팹이 설정되지 않았습니다!");
            return;
        }

        for (int i = 0; i < numberOfMonsters; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[index];

            Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        foreach (Transform point in spawnPoints)
        {
            if (point != null)
            {
                Gizmos.DrawSphere(point.position, 0.3f);
            }
        }
    }
}
