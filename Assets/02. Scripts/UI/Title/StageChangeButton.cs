using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UIManager;

public class StageChangeButton : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // 이 트리거가 이동할 스테이지 설정

    public void StageChangeButtonPunc()
    {
        Debug.Log(targetStage + "화면으로 이동!");

        // 플레이어 상태 초기화
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerStat stat = playerObj.GetComponent<PlayerStat>();
            if (stat != null)
                stat.ResetStat();
        }

        GameManager.Instance.SetStage(targetStage);
    }
}
