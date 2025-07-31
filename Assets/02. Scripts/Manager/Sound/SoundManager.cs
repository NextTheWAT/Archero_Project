using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Volume Settings")]
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance = 0.1f;
    [SerializeField][Range(0f, 1f)] private float musicVolume = 1f;
    [SerializeField][Range(0f, 1f)] private float SFX_Volum = 1f;

    [Header("BGM Clips")]
    [SerializeField] private AudioClip mainbackGroundMusic;
    [SerializeField] private AudioClip stage1Music;
    [SerializeField] private AudioClip stage2Music;
    [SerializeField] private AudioClip stage3Music;
    [SerializeField] private AudioClip stage4Music;
    [SerializeField] private AudioClip bossMusic;

    [Header("BackGround_AudioSource")]
    [SerializeField] private AudioSource BackGroundBGM;

    [Header("UISelected_AudioSource")]
    [SerializeField] private AudioSource UISelecetedSFX;

    [Header("Player_Step")]
    [SerializeField] private AudioSource Player_Step;

    public SoundSource soundSourcePrefab;

    private StageType lastStage;

    private void Start()
    {
        lastStage = GameManager.Instance.CurrentStage;
        PlayStageBGM(lastStage);
        BackGroundBGM_Set();
        UI_SFX_Set();
    }

    private void Update()
    {
        //자동 스테이지 감지 -> 음악변경
        StageType current = GameManager.Instance.CurrentStage;
        if (current != lastStage)
        {
            lastStage = current;
            PlayStageBGM(current);
        }
    }

    private void PlayStageBGM(StageType stage)
    {
        AudioClip clip = GetClipForStage(stage);
        if (clip != null && BackGroundBGM.clip != clip)
        {
            BackGroundBGM.Stop();
            BackGroundBGM.clip = clip;
            BackGroundBGM.Play();
        }
    }

    private AudioClip GetClipForStage(StageType stage)
    {
        switch (stage)
        {
            case StageType.MainStage: return mainbackGroundMusic;
            case StageType.Stage1: return stage1Music;
            case StageType.Stage2: return stage2Music;
            case StageType.Stage3: return stage3Music;
            case StageType.Stage4: return stage4Music;
            case StageType.Boss: return bossMusic;
            default: return null;
        }
    }

    public static void PlayClip(AudioClip clip)
    {
        if (clip == null) return;

        SoundSource obj = Instantiate(Instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, Instance.soundEffectVolume, Instance.soundEffectPitchVariance);
    }

    public void PlayerUISFX()
    {
        UISelecetedSFX.Play();
    }

    private void UI_SFX_Set()
    {
        UISelecetedSFX.volume = SFX_Volum;
        UISelecetedSFX.playOnAwake = false;
    }

    private void BackGroundBGM_Set()
    {
        BackGroundBGM.loop = true;
        BackGroundBGM.volume = musicVolume;
    }

    public void PlayerStep()
    {
        Player_Step.Play();
    }
}
