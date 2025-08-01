using UnityEngine;

public static class EnemyUtil
{
    public static GameObject FindNearestEnemy(Vector3 from)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");

        GameObject nearest = null;
        float minDist = float.MaxValue;

        // Enemy 검색
        foreach (GameObject enemy in enemies)
        {
            float dist = (enemy.transform.position - from).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        // Boss 검색
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
}
