using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParticleControl : MonoBehaviour
{
    public GameObject hitParticlePrefab;

    public void SpawnHitParticle(Vector3 position, Quaternion rotation)
    {
        GameObject particle = Instantiate(hitParticlePrefab, position, rotation);
        Destroy(particle, 3f);
    }

}
