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

        // 위치
        bossObj.transform.position = Vector3.zero;

        // 회전값 
        bossObj.transform.rotation = Quaternion.Euler(0, 180, 0);

        // 크기
        bossObj.transform.localScale = new Vector3(2, 2, 2);
    }

    private void Start()
    {
        // 보스가 플레이어를 알 수 있게 설정 
        playerManager = PlayerManager.Instance;
        bossMovementController = bossObj.GetComponent<BossController>();
        bossMovementController.player = playerManager.player.transform;
    }
}
