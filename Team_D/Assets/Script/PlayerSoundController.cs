using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip captureSE;
    private bool playedThisFrame = false;

    void LateUpdate()
    {
        playedThisFrame = false;
    }

    public void TryPlayCaptureSound()
    {
        Debug.Log("音鳴らすよ〜");
        if (playedThisFrame) return;

        audioSource.PlayOneShot(captureSE);
        playedThisFrame = true;
    }
}
