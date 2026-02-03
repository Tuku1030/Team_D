using UnityEngine;

public class PlayerGameOverFall : MonoBehaviour
{
    public float fallSpeed = 1.5f;
    public ParticleSystem spark;
    public AudioSource audioSource;

    bool isGameOver = false;

    void Update()
    {
        if (!isGameOver) return;

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    public void StartGameOver()
    {
        isGameOver = true;

        if (spark != null)
            spark.Play();

        if (audioSource != null)
            audioSource.Play();
    }
}