using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移動設定")]
    public float Speed = 5f;  // スピード（小さすぎたので5くらいが一般的）
    private Rigidbody2D Rbody;

    [Header("HP設定")]
    public int maxHP = 3;
    public int currentHP;
    public HeartUI heartUI;  // InspectorでHeartUIをアタッチ

    void Start()
    {
        Rbody = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        UpdateUI();
    }

    void Update()
    {
        MovePlayer();
        ClampPosition();
    }

    private void MovePlayer()
    {
        Vector2 position = transform.position;

        if (Input.GetKey(KeyCode.A)) position.x -= Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) position.x += Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.W)) position.y += Speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) position.y -= Speed * Time.deltaTime;

        transform.position = position;
    }

    private void ClampPosition()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -9.5f, 3.0f),
            Mathf.Clamp(transform.position.y, -3.8f, 4.5f)
        );
    }

    // HP操作
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        UpdateUI();

        if (currentHP == 0)
            GameOver();
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (heartUI != null)
            heartUI.UpdateHearts(currentHP);
        else
            Debug.LogWarning("HeartUI がセットされていません！");
    }

    private void GameOver()
    {
        GameOverManager.GameOver();
        // SceneManager.LoadScene("GameOverScene");
    }

    public int GetPlayerHP()
    {
        return currentHP;
    }
}
