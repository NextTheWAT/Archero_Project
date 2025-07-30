using UnityEditor;
using UnityEngine;

public enum StageType
{
    MainStage,
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    Boss
}

public class GameManager : Singleton<GameManager>
{
    public StageType CurrentStage { get; private set; } = StageType.MainStage;

    [Header("�������� ������Ʈ")]
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
    }
    private void ApplyStage(StageType stage)
    {
        // ��� �������� ��Ȱ��ȭ
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        stage4.SetActive(false);
        bossStage.SetActive(false);

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
        }

        Debug.Log($"[GameManager] ���� ��������: {stage}");
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
