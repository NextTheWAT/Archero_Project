using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UIManager;

public enum StageType
{
    MainStage,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Boss,
    Tutorial
}

public class GameManager : Singleton<GameManager>
{
    public StageType CurrentStage { get; private set; } = StageType.MainStage;
    public GameState currentState = GameState.Main;

    [Header("�������� ������Ʈ")]
    public GameObject tutorialStage;
    public GameObject mainStage;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject stage4;
    public GameObject bossStage;

    private void Start()
    {
        SetStage(CurrentStage); // ���� �������� ����
    }

    public void SetStage(StageType newStage)
    {
        if (newStage == CurrentStage) return;

        CurrentStage = newStage;
        ApplyStage(newStage);

        // ���������� ���� ���� ��ȯ
        switch (newStage)
        {
            case StageType.MainStage:
                currentState = GameState.Main;
                break;
            case StageType.Stage1:
            case StageType.Stage2:
            case StageType.Stage3:
            case StageType.Stage4:
            case StageType.Boss:
                currentState = GameState.Playing;
                break;
            case StageType.Tutorial:
                currentState = GameState.Tutorial;
                break;
        }
        // ��ġ �̵� �߰�
        PlayerSpawnManager.Instance.MovePlayerToStage(CurrentStage);

        UIManager.Instance.UpdateUI(currentState);
        // ī�޶� X ������ ���� ��û
        CameraManager.Instance?.ResetFixedX(CameraManager.Instance.playerTransform.position.x);
    }
    private void ApplyStage(StageType stage)
    {
        // ��� �������� ��Ȱ��ȭ
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        stage4.SetActive(false);
        bossStage.SetActive(false);
        tutorialStage.SetActive(false);

        // �� �������� ���� ó��
        switch (stage)
        {
            case StageType.MainStage:
                OnMainStage();
                break;
            case StageType.Stage1:
                OnStage1Start();
                break;
            case StageType.Stage2:
                OnStage2Start();
                break;
            case StageType.Stage3:
                OnStage3Start();
                break;
            case StageType.Stage4:
                OnStage4Start();
                break;
            case StageType.Boss:
                OnBossStageStart();
                break;
            case StageType.Tutorial:
                OnTutorialStageStart();
                break;
        }


        Debug.Log($"[GameManager] ���� ��������: {stage}");
    }
    private void OnTutorialStageStart()
    {
        Debug.Log("[GameManager] Ʃ�丮�� �������� Ȱ��ȭ");
        tutorialStage.SetActive(true); // �̸� �Ҵ��س��� Ʃ�丮��� ������Ʈ

        // ���� GameObject�� Ȱ��ȭ�ؾ� ��
        if (!SkillTutorialUIManager.Instance.gameObject.activeSelf)
            SkillTutorialUIManager.Instance.gameObject.SetActive(true);

        SkillTutorialUIManager.Instance.StartTutorial(); // ���� Ʃ�丮�� ���� ����
    }

    private void OnMainStage()
    {
        Debug.Log("[GameManager] ���� �������� Ȱ��ȭ");
        mainStage.SetActive(true);
    }
    private void OnStage1Start()
    {
        Debug.Log("[GameManager] �������� 1 Ȱ��ȭ");
        stage1.SetActive(true);
    }

    private void OnStage2Start()
    {
        Debug.Log("[GameManager] �������� 2 Ȱ��ȭ");
        stage2.SetActive(true);
    }

    private void OnStage3Start()
    {
        Debug.Log("[GameManager] �������� 3 Ȱ��ȭ");
        stage3.SetActive(true);
    }

    private void OnStage4Start()
    {
        Debug.Log("[GameManager] �������� 4 Ȱ��ȭ");
        stage4.SetActive(true);
    }

    private void OnBossStageStart()
    {
        Debug.Log("[GameManager] ���� �������� Ȱ��ȭ");
        bossStage.SetActive(true);
    }
}
