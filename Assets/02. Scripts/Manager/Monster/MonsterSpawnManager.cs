using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("���� ��ġ�� (�� ������Ʈ��)")]
    public Transform[] spawnPoints;

    [Header("���� ������")]
    public GameObject monsterPrefab;

    [Header("��ȯ�� ���� ��")]
    public int numberOfMonsters = 5;

    // ��ȯ�� ���� ���� ����Ʈ
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
        ClearMonsters(); // ���� ���� ���� �� ���� ��ȯ

        if (spawnPoints.Length == 0 || monsterPrefab == null)
        {
            Debug.LogWarning("���� ��ġ �Ǵ� �������� �������� �ʾҽ��ϴ�!");
            return;
        }

        for (int i = 0; i < numberOfMonsters; i++)
        {
            int index = Random.Range(0, spawnPoints.Length); // ���� ����
            Transform spawnPoint = spawnPoints[index];

            GameObject monster = Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
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
