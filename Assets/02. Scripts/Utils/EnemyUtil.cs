using UnityEngine;

public static class EnemyUtil
{
    public static GameObject FindNearestEnemy(Vector3 from)
    {
        GameObject[] enemies = SafeFindGameObjectsWithTag("Enemy");
        GameObject[] bosses = SafeFindGameObjectsWithTag("Boss");

        GameObject nearest = null;
        float minDist = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float dist = (enemy.transform.position - from).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        foreach (GameObject boss in bosses)
        {
            float dist = (boss.transform.position - from).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = boss;
            }
        }

        return nearest;
    }

    // �±װ� ������ ���� GameObject.FindGameObjectsWithTag ȣ��
    private static GameObject[] SafeFindGameObjectsWithTag(string tag)
    {
        try
        {
            return GameObject.FindGameObjectsWithTag(tag);
        }
        catch
        {
            Debug.LogWarning($"[EnemyUtil] �±� '{tag}' ��(��) ���ǵǾ� ���� �ʽ��ϴ�.");
            return new GameObject[0];
        }
    }
}
