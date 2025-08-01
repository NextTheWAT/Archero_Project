using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public bool stage1Clear = false;
    public bool stage2Clear = false;
    public bool stage3Clear = false;
    public bool stage4Clear = false;
    public bool bossStageClear = false;

    [Header("스테이지 클리어 오브젝트")]
    public GameObject stage1Object;
    public GameObject stage2Object;
    public GameObject stage3Object;
    public GameObject stage4Object;
    public GameObject bossStageObject;

    private void Start()
    {
        stage1Object.SetActive(false);
        stage2Object.SetActive(false);
        stage3Object.SetActive(false);
        stage4Object.SetActive(false);
        bossStageObject.SetActive(false);
    }

    public bool AllStagesCleared()
    {
        return stage1Clear && stage2Clear && stage3Clear && stage4Clear && bossStageClear;
    }
    public void ActivateClearedStages()
    {
        if (stage1Clear && stage1Object != null)
            stage1Object.SetActive(true);

        if (stage2Clear && stage2Object != null)
            stage2Object.SetActive(true);

        if (stage3Clear && stage3Object != null)
            stage3Object.SetActive(true);

        if (stage4Clear && stage4Object != null)
            stage4Object.SetActive(true);

        if (bossStageClear && bossStageObject != null)
            bossStageObject.SetActive(true);
    }


}
