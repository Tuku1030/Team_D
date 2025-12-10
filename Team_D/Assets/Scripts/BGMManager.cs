using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    public AudioSource bgm;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーンをまたいでも残る
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ステージの曲に切り替える
    public void ChangeMusic(AudioClip newClip)
    {
        if (bgm.clip == newClip) return; // 同じ曲なら何もしない

        bgm.clip = newClip;
        bgm.Play();
    }
}
