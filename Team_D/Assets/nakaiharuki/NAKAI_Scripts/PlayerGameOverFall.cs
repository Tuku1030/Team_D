using UnityEngine;

public class PlayerGameOverFall : MonoBehaviour
{
    public float fallSpeed = 1.5f;
    public float rotateSpeed = 200f;
    public ParticleSystem spark;

    bool isGameOver = false;

    void Update()
    {
        if (!isGameOver) return;

        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
       
    }

    public void StartGameOver()
    {
        isGameOver = true;
        if (spark != null) spark.Play();
    }
}