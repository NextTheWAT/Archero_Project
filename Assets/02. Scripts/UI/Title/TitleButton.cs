using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void TitleChangeButton()
    {
        Debug.Log("Ÿ��Ʋ ȭ������!");
        GameManager.Instance.SetStage(StageType.MainStage);
    }
}
