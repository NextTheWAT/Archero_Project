using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("�÷��̾ �̵��� ���� ��ġ")]
    [SerializeField] private Transform spawnPosition;

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.position = spawnPosition.position;
    }
}
