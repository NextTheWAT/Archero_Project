using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance = null;
    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null) return null;
            return _instance;
        }
    }

    public GameObject playerPrefab;
    public PlayerMovement player;

    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            Init();
        }
    }

    private void Init()
    {
        GameObject playerObj = Instantiate(playerPrefab, transform);
        player = playerObj.GetComponent<PlayerMovement>();

        // 위치
        playerObj.transform.position = new Vector3(0, 0, -10);

        // 회전값 
        playerObj.transform.rotation = Quaternion.Euler(0, 0, 0);

        // 크기
        playerObj.transform.localScale = new Vector3(1.7f, 1, 7f);
    }
}
