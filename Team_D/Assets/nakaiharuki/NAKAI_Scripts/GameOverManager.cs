using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static string lastStage;
    public static GameOverManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void GameOver()
    {//今のステージ名保存
        lastStage = SceneManager.GetActiveScene().name;

        Instance.StartCoroutine(Instance.GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        // プレイヤーをタグ "Player" で取得
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            var pc = player.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;

            // 演出スクリプト追加（ここで初めて動く）
            player.AddComponent<PlayerExitFadeMover>();
        }
    

        // ① 画面を黒くフェード（1秒）
        yield return ScreenFade.Instance.StartCoroutine(ScreenFade.Instance.FadeOut(2f));

        // ② 完全に黒くなったらゲームオーバー画面へ
        SceneManager.LoadScene("GameOver");
    }
   
    

    public void RestartGame()
    {
        SceneManager.LoadScene(lastStage);
    }
}