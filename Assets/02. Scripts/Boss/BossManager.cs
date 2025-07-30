using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private static BossManager _instance = null;
    public static BossManager Instance
    {
        get
        {
            if (_instance == null) return null;
            return _instance;
        }
    }

    public GameObject bossPrefab;
    public GameObject bossObj;

    public BossController bossMovementController;

    public PlayerManager playerManager;

    private void Awake()
    {

        if(_instance == null)
        {
            _instance = this;
            Init();
        }
    }

    private void Init()
    {
        bossObj = Instantiate(bossPrefab, transform);

        // ��ġ
        bossObj.transform.position = Vector3.zero;

        // ȸ���� 
        bossObj.transform.rotation = Quaternion.Euler(0, 180, 0);

        // ũ��
        bossObj.transform.localScale = new Vector3(2, 2, 2);
    }

    private void Start()
    {
        // ������ �÷��̾ �� �� �ְ� ���� 
        playerManager = PlayerManager.Instance;
        bossMovementController = bossObj.GetComponent<BossController>();
        bossMovementController.player = playerManager.player.transform;
    }
}
