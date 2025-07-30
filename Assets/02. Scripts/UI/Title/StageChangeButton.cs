using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageChangeButton : MonoBehaviour
{
    [SerializeField] private StageType targetStage; // 이 트리거가 이동할 스테이지 설정

    public void StageChangeButtonPunc()
    {
        Debug.Log(targetStage + "화면으로 이동!");
        GameManager.Instance.SetStage(targetStage);
    }
}
