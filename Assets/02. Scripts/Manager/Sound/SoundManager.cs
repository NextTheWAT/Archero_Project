using OpenCover.Framework.Model;
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

    [Header("UI_AudioSource\n0 : ��ư ���� ���� / 1 : ������ ���� / 2 : �¸� / 3 : �й�")]
    [SerializeField] private List<AudioSource> UISelecetedSFX;

    [Header("Player_AudioSource\n0 : ���� / 1 : �� / 2 : ��Ʈ")]
    [SerializeField] private List<AudioSource> Player_Sfx; // 0 : ���� 1 : �� 2 : ��Ʈ

    [Header("Moster_AudioSource\n0 : ���� / 1 : ��Ʈ / 2 : ����")]
    [SerializeField] private List<AudioSource> Monster_Sfx; 

    [Header("Boss_AudioSource0 : ���� / 1 : ��Ʈ / 2 : ����")]
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
        //�ڵ� �������� ���� -> ���Ǻ���
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


    /// <summary>
    /// 0 : ��ư ���� ���� / 1 : ������ ���� / 2 : �¸� / 3 : �й�  
    /// </summary>
    /// <param name="num"></param>
    public void UI_Select_SFX(int num)
    {
        UISelecetedSFX[num].Play();

        // if 
    }

    /// <summary>
    /// 0 �� StepSound / 1 �� Shooting / 2 �� Hit
    /// </summary>
    /// <param name="num"></param>
    public void Player_SFX(int num)
    {
        Player_Sfx[num].Play();
    }

    /// <summary>
    /// 0 : AttackSound / 1 : HitSound / 2 : DieSound
    /// </summary>
    /// <param name="num"></param>
    public void Monster_SFX(int num)
    {
        Monster_Sfx[num].Play();
    }

    /// <summary>
    /// 0 : AttackSound / 1 : HitSound / 2 : DieSound
    /// </summary>
    /// <param name="num"></param>
    public void Boss_SFX(int num)
    {
        Boss_Sfx[num].Play();
    }

    /// <summary>
    /// Volume�� volume�� �־��ֽø� �˴ϴ�.
    /// </summary>
    /// <param name="volume"></param>
    private void SFX_Set(float volume)
    {
        void SetVolumForList(List<AudioSource> sfxList)
        {
            foreach (AudioSource sfx in sfxList)
            {
                sfx.volume = volume;
                sfx.playOnAwake = false;
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
