using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("���� ��ġ�� (�� ������Ʈ��)")]
    public Transform[] spawnPoints;

    [Header("���� ������")]
    public GameObject monsterPrefab;

    [Header("��ȯ�� ���� ��")]
    public int numberOfMonsters = 5;

    [Header("���� �� �ڵ� ��ȯ ����")]
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
            Debug.LogWarning("���� ��ġ �Ǵ� �������� �������� �ʾҽ��ϴ�!");
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
