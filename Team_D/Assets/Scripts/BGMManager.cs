using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    [Header("AudioSources")]
    public AudioSource bgmSource;   // BGM再生用
    public AudioSource seSource;    // SE再生用

    [Header("AudioMixerGroups")]
    public AudioMixerGroup bgmMixerGroup;
    public AudioMixerGroup seMixerGroup;

    [Header("BGM Clips")]
    public AudioClip titleBGM;
    public AudioClip stage1BGM;
    public AudioClip stage2BGM;
    public AudioClip stage3BGM;
    public AudioClip resultBGM;
    public AudioClip optionBGM;
    public AudioClip gameOverBGM;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // AudioSourceがアタッチされていなければ自動作成
            if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
            if (seSource == null) seSource = gameObject.AddComponent<AudioSource>();

            bgmSource.outputAudioMixerGroup = bgmMixerGroup;
            seSource.outputAudioMixerGroup = seMixerGroup;

            SceneManager.sceneLoaded += OnSceneLoaded;

            if (bgmSource.clip == null) PlayTitleBGM();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // --------------------
    // シーン切り替えでBGM再生
    // --------------------
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Title":
            case "StageSelect": PlayTitleBGM(); break;
            case "Stage1": PlayStageBGM(1); break;
            case "Stage2": PlayStageBGM(2); break;
            case "Stage3": PlayStageBGM(3); break;
            case "Result": PlayResultBGM(); break;
            case "Option": PlayOptionBGM(); break;
            case "GameOver": PlayGameOverBGM(); break;
        }
    }

    // --------------------
    // BGM制御
    // --------------------
    public void ChangeMusic(AudioClip clip, bool loop = true)
    {
        if (bgmSource == null || clip == null) return;
        if (bgmSource.clip == clip) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void PlayTitleBGM() => ChangeMusic(titleBGM);
    public void PlayStageBGM(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1: ChangeMusic(stage1BGM); break;
            case 2: ChangeMusic(stage2BGM); break;
            case 3: ChangeMusic(stage3BGM); break;
        }
    }
    public void PlayResultBGM() => ChangeMusic(resultBGM);
    public void PlayOptionBGM() => ChangeMusic(optionBGM);
    public void PlayGameOverBGM() => ChangeMusic(gameOverBGM);
    public void StopBGM() { if (bgmSource == null) return; bgmSource.Stop(); }

    // --------------------
    // SE制御
    // --------------------
    public void PlaySE(AudioClip clip)
    {
        if (seSource == null || clip == null) return;
        seSource.PlayOneShot(clip);
    }

    // --------------------
    // 音量スライダー対応
    // --------------------
    public void SetBGMVolume(float volume)
    {
        if (bgmMixerGroup?.audioMixer == null) return;

        if (volume <= 0f)
            bgmMixerGroup.audioMixer.SetFloat("BGM", 0f);
        else
            bgmMixerGroup.audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20f);
    }

    public void SetSEVolume(float volume)
    {
        if (seMixerGroup?.audioMixer == null) return;

        if (volume <= 0f)
            seMixerGroup.audioMixer.SetFloat("SE", 0f);
        else
            seMixerGroup.audioMixer.SetFloat("SE", Mathf.Log10(volume) * 20f);
    }
}
