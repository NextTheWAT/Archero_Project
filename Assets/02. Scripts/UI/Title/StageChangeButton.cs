using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageChangeButton : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // �� Ʈ���Ű� �̵��� �������� ����

    public void StageChangeButtonPunc()
    {
        Debug.Log(targetStage + "ȭ������ �̵�!");
        GameManager.Instance.SetStage(targetStage);
    }
}
