using System.Collections.Generic;
using UnityEngine;

public class NetScoreCalculator : MonoBehaviour
{
    private const float baseScore = 1f;
    private float totalScoreMultiplier = 1f; // ���݂̖ԃX�R�A�{��

    // ����ߊl�����Ƃ��ɌĂ΂�郁�\�b�h
    public void CaptureFishes(List<Fish> fishes)
    {
        if (fishes == null || fishes.Count == 0) return;

        // ��ނ��ƂɏW�v
        Dictionary<string, (int count, float addRate)> fishCounts = new();

        foreach (var fish in fishes)
        {
            if (!fishCounts.ContainsKey(fish.fishName))
            {
                fishCounts[fish.fishName] = (1, fish.addRate);
            }
            else
            {
                var current = fishCounts[fish.fishName];
                fishCounts[fish.fishName] = (current.count + 1, current.addRate);
            }
        }

        // �����Ƃ̔{�����Z�����v
        float addTotal = 0f;
        foreach (var kvp in fishCounts)
        {
            var (count, rate) = kvp.Value;
            if (count > 1)
            {
                float add = (count - 1) * rate;
                Debug.Log($"{kvp.Key} �̔{�����Z: {add}");
                addTotal += add;
            }
        }

        totalScoreMultiplier = baseScore + addTotal;
        Debug.Log($"�ŏI�X�R�A�{��: {totalScoreMultiplier}");

        // �ߊl���������폜
        foreach (var fish in fishes)
        {
            Destroy(fish.gameObject);
        }
    }

    public float GetCurrentScoreMultiplier()
    {
        return totalScoreMultiplier;
    }
}