using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UIManager;

public class StateChangeButton : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    public void StateChangeButtonPunc()
    {
        Debug.Log(gameState + "상태로 변경!");
        UIManager.Instance.UpdateUI(gameState);
    }
}
