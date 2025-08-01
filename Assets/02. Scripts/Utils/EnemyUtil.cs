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

    // 태그가 존재할 때만 GameObject.FindGameObjectsWithTag 호출
    private static GameObject[] SafeFindGameObjectsWithTag(string tag)
    {
        try
        {
            return GameObject.FindGameObjectsWithTag(tag);
        }
        catch
        {
            Debug.LogWarning($"[EnemyUtil] 태그 '{tag}' 이(가) 정의되어 있지 않습니다.");
            return new GameObject[0];
        }
    }
}
