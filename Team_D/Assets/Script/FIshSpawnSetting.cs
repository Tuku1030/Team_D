using UnityEngine;

[System.Serializable]
public class FishSpawnSetting
{
    public GameObject prefab;
    public int maxCount = 5;
    public float spawnInterval = 3f;

    [HideInInspector]
    public int currentCount = 0;

    [HideInInspector]
    public float timer = 0f;
}
