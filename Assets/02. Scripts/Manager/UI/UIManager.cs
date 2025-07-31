using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public enum GameState
    {
        Main,
        Playing,
        GameOver,
        Tutorial
    }

    [Header("Main UI")]
    public GameObject titleCanvas;

    [Header("Playing UI")]
    public GameObject playerUI;
    public GameObject skillUIManager;

    [Header("GameOver UI")]
    public GameObject gameOverCanvas;

    [Header("Tutorial UI")]
    public GameObject tutorialCanvas;

    private Dictionary<GameState, GameObject[]> stateToUI;
    private GameState currentState = GameState.Main;


    private void Awake()
    {
        stateToUI = new Dictionary<GameState, GameObject[]>
        {
            { GameState.Main, new[] { titleCanvas } },
            { GameState.Playing, new[] { playerUI, skillUIManager, tutorialCanvas } },
            { GameState.GameOver, new[] { gameOverCanvas } },
            { GameState.Tutorial, new[] { tutorialCanvas } }
        };
    }
    private void Start()
    {
        UpdateUI(GameState.Main);
    }

    public void UpdateUI(GameState newState)
    {
        currentState = newState; // ���� ����

        // ��ü UI ��Ȱ��ȭ
        titleCanvas.SetActive(false);
        playerUI.SetActive(false);
        skillUIManager.SetActive(false);
        gameOverCanvas.SetActive(false);
        tutorialCanvas.SetActive(false);

        // ���� ���� UI�� Ȱ��ȭ
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