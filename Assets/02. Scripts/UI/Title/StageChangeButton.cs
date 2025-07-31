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
        Debug.Log(targetStage + "ȭ������ �̵�!");

        // �÷��̾� ���� �ʱ�ȭ
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
