using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;      // フェード用のImage
    public float fadeSpeed = 1f; // フェードの速さ



    public void FadeOutToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
    IEnumerator FadeOut(string sceneName)
    {
        Color c = fadeImage.color;
        // フェードアウト（透明→黒）
        while (c.a < 1f)
        {
            c.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return null;
        }
        // シーンを切り替え
        SceneManager.LoadScene(sceneName);
    }
} 