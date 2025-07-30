using DG.Tweening.Core.Easing;
using UnityEditor;
using UnityEngine;
using static UnityEditorInternal.VersionControl.ListControl;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    private AudioSource musicAudioSource;
    [SerializeField] private AudioClip backGroundMusic;
    [SerializeField] private AudioClip stage1Mudic;
    [SerializeField] private AudioClip stage2Mudic;
    [SerializeField] private AudioClip stage3Mudic;
    [SerializeField] private AudioClip stage4Mudic;
    [SerializeField] private AudioClip bossMudic;

    public SoundSource soundSourcePrefab;
    //private StageType lastStage;      //게임매니저에서 가져올 상태기준으로 배경음악 체인지


    private void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.loop = true;
    }

    private void Start()
    {
        //lastStage = GameManager.Instance.CurrentStage;
        //PlayStageBGM(lastStage);
    }

    private void Update()
    {
        //StageType current = GameManager.Instance.CurrentStage;
        //if (current != lastStage)
        //{
        //    lastStage = current;
        //    PlayStageBGM(current);
        //}
    }


    public void ChangeBackGroundMusic(AudioClip clip)
    {
        musicAudioSource.Stop();
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        SoundSource obj = Instantiate(Instance.soundSourcePrefab);
        SoundSource soundSource = obj.GetComponent<SoundSource>();
        soundSource.Play(clip, Instance.soundEffectVolume, Instance.soundEffectPitchVariance);
    }
    //private void PlayStageBGM(StageType stage)
    //{
    //    AudioClip clip = GetClipForStage(stage);
    //    if (clip != null)
    //    {
    //        ChangeBackGroundMusic(clip);
    //    }
    //}

    //public void ChangeBackGroundMusic(AudioClip clip)
    //{
    //    if (musicAudioSource.clip == clip) return;

    //    musicAudioSource.Stop();
    //    musicAudioSource.clip = clip;
    //    musicAudioSource.Play();
    //}
    //private AudioClip GetClipForStage(StageType stage)
    //{
    //    switch (stage)
    //    {
    //        case StageType.Stage1: return stage1Clip;
    //        case StageType.Stage2: return stage2Clip;
    //        case StageType.Stage3: return stage3Clip;
    //        case StageType.Stage4: return stage4Clip;
    //        case StageType.Boss: return bossClip;
    //        default: return null;
    //    }
    //}
}