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
        if (isGameOver) return;
        isGameOver = true;

        lastStage = SceneManager.GetActiveScene().name;

        if (Instance != null)  // ←ここでチェック
        {
            Instance.StartCoroutine(Instance.GameOverSequence());
        }
        else
        {
            Debug.LogError("GameOverManager の Instance が存在しません！");
        }
    }


    private IEnumerator GameOverSequence()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            var pc = player.GetComponent<PlayerController>();
            if (pc != null) pc.enabled = false;

            // ここで落下＋火花演出スタート
            var fall = player.GetComponent<PlayerGameOverFall>();
            if (fall != null) fall.StartGameOver();
        }

        // 演出を見る時間
        yield return new WaitForSeconds(2f);

        // フェード
        yield return ScreenFade.Instance.StartCoroutine(
            ScreenFade.Instance.FadeOut(1f)
        );

        SceneManager.LoadScene("GameOver");
    }
        public void RestartGame()
    {
        isGameOver = false;          //次のゲーム用にリセット
        Time.timeScale = 1f;

        SceneManager.LoadScene(lastStage);
    }
}
    
