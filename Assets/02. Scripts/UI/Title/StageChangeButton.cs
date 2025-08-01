using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UIManager;

public class StageChangeButton : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // �� Ʈ���Ű� �̵��� �������� ����

    public void StageChangeButtonPunc()
    {
        Debug.Log(targetStage + " ȭ������ �̵� �õ�!");

        // ���� targetStage�� Ŭ����ƴ��� Ȯ��
        if (IsStageCleared(targetStage))
        {
            Debug.Log($"{targetStage}�� �̹� Ŭ�����. ��Ŭ���� ���������� ��ü��.");

            // Stage1 ~ Boss���� ��ȸ�ϸ鼭 ���� Ŭ������� ���� �������� ã��
            foreach (StageType stage in System.Enum.GetValues(typeof(StageType)))
            {
                if (stage == StageType.MainStage)
                    continue; // ����

                if (!IsStageCleared(stage))
                {
                    targetStage = stage;
                    Debug.Log($"�̵��� ��Ŭ���� �������� �߰� �� {targetStage}");
                    break;
                }
            }
        }

        // �÷��̾� ���� �ʱ�ȭ
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
            _ => true // MainStage, Tutorial ���� Ŭ���� ���� ����
        };
    }
}