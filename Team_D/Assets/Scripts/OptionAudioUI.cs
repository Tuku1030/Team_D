using UnityEngine;
using UnityEngine.UI;

public class OptionAudioUI : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    void Start()
    {
        // 🔒 安全チェック
        if (BGMManager.Instance == null)
        {
            Debug.LogError("BGMManager が存在しません（Titleから再生してね）");
            return;
        }

        if (bgmSlider == null || seSlider == null)
        {
            Debug.LogError("Slider が設定されていません");
            return;
        }

        var bgm = BGMManager.Instance;

        // 🎯 現在の音量をスライダーに反映
        bgmSlider.value = bgm.CurrentBGMVolume;
        seSlider.value = bgm.CurrentSEVolume;

        // 🔁 リスナー登録（多重防止）
        bgmSlider.onValueChanged.RemoveAllListeners();
        seSlider.onValueChanged.RemoveAllListeners();

        bgmSlider.onValueChanged.AddListener(bgm.SetBGMVolume);
        seSlider.onValueChanged.AddListener(bgm.SetSEVolume);
    }
}
