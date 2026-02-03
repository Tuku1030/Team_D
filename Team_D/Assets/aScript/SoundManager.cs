using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource seSource;
    public AudioClip fishSE;
    public AudioClip emptySE;

    public void PlayNetResultSE(bool hasTrash, bool hasAnyCatch)
    {
        if (hasTrash || !hasAnyCatch)
            seSource.PlayOneShot(emptySE);
        else
            seSource.PlayOneShot(fishSE);
    }
}
