using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParticleControl : MonoBehaviour
{
    public GameObject hitParticlePrefab;
    
    public void SpawnHitParticle(Vector3 position)
    {
        // 파티클 생성
        GameObject particle = Instantiate(hitParticlePrefab, position, Quaternion.identity);

        // 3초 뒤 파티클 오브젝트 삭제
        Destroy(particle, 3f);
    }
}
