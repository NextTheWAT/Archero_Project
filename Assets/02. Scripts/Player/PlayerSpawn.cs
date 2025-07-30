using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("플레이어가 이동할 스폰 위치")]
    [SerializeField] private Transform spawnPosition;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = spawnPosition.position;
    }
}
