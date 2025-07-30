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
    public StageType CurrentStage { get; private set; } = StageType.Stage1;

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
    private void OnStage1Start()
    {
        stage1.SetActive(true);
        // TODO: ������� ����, UI �ʱ�ȭ ��
    }

    private void OnStage2Start()
    {
        stage2.SetActive(true);
        // TODO: �ٸ� ���� ���, ���̵� ���� ��
    }

    private void OnStage3Start()
    {
        stage3.SetActive(true);
    }

    private void OnStage4Start()
    {
        stage4.SetActive(true);
    }

    private void OnBossStageStart()
    {
        bossStage.SetActive(true);
        // TODO: ���� ���� ����, ī�޶� ��, ���� ���� ��
    }
}
