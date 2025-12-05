using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance;

    private Image fadeImage;

    void Awake()
    {
        Instance = this;
        fadeImage = GetComponent<Image>();
    }

    // フェードアウト（画面が黒くなる）
    public IEnumerator FadeOut(float duration)
    {
        float timer = 0f;
        Color c = fadeImage.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, timer / duration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;
    }
}