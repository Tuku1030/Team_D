using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    [Header("AudioSources")]
    public AudioSource bgmSource;
    public AudioSource seSource;

    [Header("AudioMixerGroups")]
    public AudioMixerGroup bgmMixerGroup;
    public AudioMixerGroup seMixerGroup;

    [Header("BGM Clips")]
    public AudioClip titleBGM;
    public AudioClip stage1BGM;
    public AudioClip stage2BGM;
    public AudioClip stage3BGM;
    public AudioClip resultBGM;
    public AudioClip gameOverBGM;

    // 🔊 音量範囲（dB）
    private const float MIN_VOLUME = -80f;
    private const float MAX_VOLUME = 0f;

    // 🎚 現在の音量（UIと同期用）
    public float CurrentBGMVolume { get; private set; } = 0f;
    public float CurrentSEVolume { get; private set; } = 0f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
        if (seSource == null) seSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = bgmMixerGroup;
        seSource.outputAudioMixerGroup = seMixerGroup;

        // 🔊 初期音量を反映（BGMもSEも同じ）
        SetBGMVolume(CurrentBGMVolume);
        SetSEVolume(CurrentSEVolume);

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (bgmSource.clip == null)
            PlayTitleBGM();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // --------------------
    // シーンごとのBGM切替
    // --------------------
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Title":
            case "StageSelect":
                PlayTitleBGM();
                break;
            case "Stage1": PlayStageBGM(1); break;
            case "Stage2": PlayStageBGM(2); break;
            case "Stage3": PlayStageBGM(3); break;
            case "Result": PlayResultBGM(); break;
            case "GameOver": PlayGameOverBGM(); break;
        }
    }

    // --------------------
    // BGM制御
    // --------------------
    public void ChangeMusic(AudioClip clip)
    {
        if (bgmSource == null || clip == null) return;
        if (bgmSource.clip == clip) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlayTitleBGM() => ChangeMusic(titleBGM);

    public void PlayStageBGM(int stage)
    {
        if (stage == 1) ChangeMusic(stage1BGM);
        if (stage == 2) ChangeMusic(stage2BGM);
        if (stage == 3) ChangeMusic(stage3BGM);
    }

    public void PlayResultBGM() => ChangeMusic(resultBGM);
    public void PlayGameOverBGM() => ChangeMusic(gameOverBGM);

    // --------------------
    // SE
    // --------------------
    public void PlaySE(AudioClip clip)
    {
        if (seSource == null || clip == null) return;
        seSource.PlayOneShot(clip);
    }

    // --------------------
    // UI（ボタン）用SE
    // --------------------
    [Header("UI Button SE")]
    public AudioClip buttonClickSE;

    // ボタンが押されたときに呼ぶ専用関数
    public void PlayButtonClickSE()
    {
        if (seSource == null || buttonClickSE == null) return;

        seSource.PlayOneShot(buttonClickSE);
    }

    // --------------------
    // 🔊 音量調整（dB）
    // --------------------
    public void SetBGMVolume(float volume)
    {
        if (bgmMixerGroup?.audioMixer == null) return;

        volume = Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME);
        CurrentBGMVolume = volume;

        bgmMixerGroup.audioMixer.SetFloat("BGMVolume", volume);
    }

    public void SetSEVolume(float volume)
    {
        if (seMixerGroup?.audioMixer == null) return;

        volume = Mathf.Clamp(volume, MIN_VOLUME, MAX_VOLUME);
        CurrentSEVolume = volume;

        seMixerGroup.audioMixer.SetFloat("SEVolume", volume);
    }
}
