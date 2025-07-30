using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public enum GameState
    {
        Main,
        Playing,
        GameOver
    }

    [Header("Main UI")]
    public GameObject titleCanvas;

    [Header("Playing UI")]
    public GameObject playerStatCanvas;
    public GameObject playerHpBarCanvas;
    public GameObject skillUIManager;

    [Header("GameOver UI")]
    //public GameObject gameOverCanvas;

    private Dictionary<GameState, GameObject[]> stateToUI;
    private GameState currentState = GameState.Main;


    private void Awake()
    {
        stateToUI = new Dictionary<GameState, GameObject[]>
        {
            { GameState.Main, new[] { titleCanvas } },
            { GameState.Playing, new[] { playerStatCanvas, playerHpBarCanvas, skillUIManager } },
            //{ GameState.GameOver, new[] { gameOverCanvas } }
        };
    }

    public void UpdateUI(GameState newState)
    {
        currentState = newState; // 상태 저장

        // 전체 UI 비활성화
        titleCanvas.SetActive(false);
        playerStatCanvas.SetActive(false);
        playerHpBarCanvas.SetActive(false);
        skillUIManager.SetActive(false);

        // 현재 상태 UI만 활성화
        if (stateToUI.TryGetValue(newState, out var uiList))
        {
            foreach (var ui in uiList)
                ui.SetActive(true);
        }
    }
public GameState GetCurrentState()
    {
        return currentState;
    }
}