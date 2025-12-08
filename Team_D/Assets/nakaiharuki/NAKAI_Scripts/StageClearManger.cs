using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StageClearManager : MonoBehaviour
{
    public static bool isClear = false;

    public void StageClear()
    {
        if (isClear) return;   // 2回呼ばれ防止
        isClear = true;

        StartCoroutine(ClearSequence());
    }

    IEnumerator ClearSequence()
    {
        // プレイヤー操作停止
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            var pc = player.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;
            player.AddComponent<PlayerExitFadeMover>();
        }

        // フェードアウト（2秒）
        yield return ScreenFade.Instance.StartCoroutine(
            ScreenFade.Instance.FadeOut(2f)
        );

        // リザルトへ
        SceneManager.LoadScene("result");
    }
}