using System.Collections.Generic;
using UnityEngine;
using static MonsterFSM;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("스폰 위치들 (빈 오브젝트들)")]
    public Transform[] spawnPoints;

    [Header("몬스터 프리팹들")]
    public GameObject[] monsterPrefabs;

    [Header("소환할 몬스터 수")]
    public int numberOfMonsters = 5;

    // 소환된 몬스터 추적 리스트
    private List<GameObject> spawnedMonsters = new List<GameObject>();

    private void OnEnable()
    {
        SpawnMonsters();
    }

    private void OnDisable()
    {
        ClearMonsters();
    }


    public void SpawnMonsters()
    {
        ClearMonsters(); // 기존 몬스터 제거 후 새로 소환

        if (spawnPoints.Length == 0 || monsterPrefabs == null || monsterPrefabs.Length == 0)
        {
            Debug.LogWarning("스폰 위치 또는 몬스터 프리팹 배열이 설정되지 않았습니다!");
            return;
        }

        for (int i = 0; i < numberOfMonsters; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            int prefabIndex = Random.Range(0, monsterPrefabs.Length);

            Transform spawnPoint = spawnPoints[spawnIndex];
            GameObject prefabToSpawn = monsterPrefabs[prefabIndex];

            GameObject monster = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
            spawnedMonsters.Add(monster);
        }
    }

    private void ClearMonsters()
    {
        foreach (GameObject monster in spawnedMonsters)
        {
            if (monster != null)
                Destroy(monster);
        }
        spawnedMonsters.Clear();
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
