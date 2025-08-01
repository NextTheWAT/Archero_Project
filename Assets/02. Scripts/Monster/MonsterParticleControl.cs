using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParticleControl : MonoBehaviour
{
    public GameObject hitParticlePrefab;
    
    public void SpawnHitParticle(Vector3 position)
    {
        // ��ƼŬ ����
        GameObject particle = Instantiate(hitParticlePrefab, position, Quaternion.identity);

        // 3�� �� ��ƼŬ ������Ʈ ����
        Destroy(particle, 3f);
    }
}
