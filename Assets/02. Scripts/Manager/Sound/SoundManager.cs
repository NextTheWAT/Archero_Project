using System.Collections.Generic;
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

    [Header("UI_AudioSource\n0 : 버튼 선택 사운드 | 1 : 레벨업 사운드 | 2 : 게임 결과")]
    [SerializeField] private List<AudioSource> UISelecetedSFX;

    [Header("Player_AudioSource\n0 : 스텝 | 1 : 슛 | 2 : 히트")]
    [SerializeField] private List<AudioSource> Player_Sfx; // 0 : 스텝 1 : 슛 2 : 히트

    [Header("Moster_AudioSource\n0 : 공격 | 1 : 히트 | 2 : 다이")]
    [SerializeField] private List<AudioSource> Monster_Sfx; 

    [Header("Boss_AudioSource0 : 공격 | 1 : 히트 | 2 : 다이")]
    [SerializeField] private List<AudioSource> Boss_Sfx;


    [Space(20f)]


    public SoundSource soundSourcePrefab;

    private StageType lastStage;

    private void Start()
    {
        lastStage = GameManager.Instance.CurrentStage;
        PlayStageBGM(lastStage);
    }

    private void Update()
    {
        BackGroundBGM_Set();
        SFX_Set(SFX_Volum);
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

    public void UI_Select_SFX()
    {
        UISelecetedSFX[0].Play();
    }
    public void PlayerStep_SFX()
    {
        Player_Sfx[0].Play();
    }

    public void PlayerShooting_SFX()
    {
        Player_Sfx[1].Play();
    }

    public void Player_TakeDamage()
    {
        Player_Sfx[2].Play();
    }

    private void SFX_Set(float volume)
    {
        void SetVolumForList(List<AudioSource> sfxList)
        {
            foreach (AudioSource sfx in sfxList)
            {
                sfx.volume = volume;
            }
        }

        SetVolumForList(UISelecetedSFX);
        SetVolumForList(Player_Sfx);
        SetVolumForList(Monster_Sfx);
        SetVolumForList(Boss_Sfx);
    }

    private void BackGroundBGM_Set()
    {
        BackGroundBGM.loop = true;
        BackGroundBGM.volume = musicVolume;
    }

    
}
