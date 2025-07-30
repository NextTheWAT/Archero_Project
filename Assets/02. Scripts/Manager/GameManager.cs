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

    [Header("스테이지 오브젝트")]
    public GameObject mainStage;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    public GameObject stage4;
    public GameObject bossStage;

    private void Start()
    {
        SetStage(CurrentStage); // 시작 스테이지 설정
    }

    public void SetStage(StageType newStage)
    {
        if (newStage == CurrentStage) return;

        CurrentStage = newStage;
        ApplyStage(newStage);
    }
    private void ApplyStage(StageType stage)
    {
        // 모든 스테이지 비활성화
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(false);
        stage4.SetActive(false);
        bossStage.SetActive(false);

        // 각 스테이지 전용 처리
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

        Debug.Log($"[GameManager] 현재 스테이지: {stage}");
    }
    private void OnMainStage()
    {
        Debug.Log("[GameManager] 메인 스테이지 활성화");
        mainStage.SetActive(true);
    }
    private void OnStage1Start()
    {
        Debug.Log("[GameManager] 스테이지 1 활성화");
        stage1.SetActive(true);
    }

    private void OnStage2Start()
    {
        Debug.Log("[GameManager] 스테이지 2 활성화");
        stage2.SetActive(true);
    }

    private void OnStage3Start()
    {
        Debug.Log("[GameManager] 스테이지 3 활성화");
        stage3.SetActive(true);
    }

    private void OnStage4Start()
    {
        Debug.Log("[GameManager] 스테이지 4 활성화");
        stage4.SetActive(true);
    }

    private void OnBossStageStart()
    {
        Debug.Log("[GameManager] 보스 스테이지 활성화");
        bossStage.SetActive(true);
    }
}
