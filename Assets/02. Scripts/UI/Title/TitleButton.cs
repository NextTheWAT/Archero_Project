using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void TitleChangeButton()
    {
        Debug.Log("타이틀 화면으로!");
        GameManager.Instance.SetStage(StageType.MainStage);
    }
}
