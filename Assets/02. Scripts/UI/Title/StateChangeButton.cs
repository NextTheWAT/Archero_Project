using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class StateChangeButton : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    public void StateChangeButtonPunc()
    {
        Debug.Log(gameState + "���·� ����!");
        UIManager.Instance.UpdateUI(gameState);
    }
}
