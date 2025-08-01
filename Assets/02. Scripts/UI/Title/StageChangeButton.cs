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
        Debug.Log(targetStage + " 화면으로 이동 시도!");

        // 먼저 targetStage가 클리어됐는지 확인
        if (IsStageCleared(targetStage))
        {
            Debug.Log($"{targetStage}는 이미 클리어됨. 미클리어 스테이지로 대체함.");

            // Stage1 ~ Boss까지 순회하면서 아직 클리어되지 않은 스테이지 찾기
            foreach (StageType stage in System.Enum.GetValues(typeof(StageType)))
            {
                if (stage == StageType.MainStage)
                    continue; // 제외

                if (!IsStageCleared(stage))
                {
                    targetStage = stage;
                    Debug.Log($"이동할 미클리어 스테이지 발견 → {targetStage}");
                    break;
                }
            }
        }

        // 플레이어 상태 초기화
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            PlayerStat stat = playerObj.GetComponent<PlayerStat>();
            if (stat != null)
                stat.ResetStat();
        }

        GameManager.Instance.SetStage(targetStage);
        SoundManager.Instance.UI_Select_SFX(0);
    }

    private bool IsStageCleared(StageType stage)
    {
        return stage switch
        {
            StageType.Stage1 => StageManager.Instance.stage1Clear,
            StageType.Stage2 => StageManager.Instance.stage2Clear,
            StageType.Stage3 => StageManager.Instance.stage3Clear,
            StageType.Stage4 => StageManager.Instance.stage4Clear,
            StageType.Boss => StageManager.Instance.bossStageClear,
            _ => true // MainStage, Tutorial 등은 클리어 개념 없음
        };
    }
}