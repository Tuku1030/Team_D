using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance;
    public AudioSource audioSource;

    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    public void PlaySE(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
