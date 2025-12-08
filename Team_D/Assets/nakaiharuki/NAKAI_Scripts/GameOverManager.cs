using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public static string lastStage;
    public static GameOverManager Instance;
    public static bool isGameOver = false;

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
    {
        //二重実行防止（超重要）
        if (isGameOver) return;
        isGameOver = true;

        //「今いるステージ名」を正しく保存
        lastStage = SceneManager.GetActiveScene().name;

        Instance.StartCoroutine(Instance.GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        //プレイヤー停止 + 演出
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            var pc = player.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;

            player.AddComponent<PlayerExitFadeMover>();
        }

        //画面フェード
        yield return ScreenFade.Instance.StartCoroutine(
            ScreenFade.Instance.FadeOut(2f)
        );

        //GameOver シーンへ
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        isGameOver = false;          //次のゲーム用にリセット
        Time.timeScale = 1f;

        SceneManager.LoadScene(lastStage);
    }
}