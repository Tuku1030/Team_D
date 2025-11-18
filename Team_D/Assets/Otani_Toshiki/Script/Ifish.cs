using UnityEngine;

namespace FishGame
{
    public interface IFish
    {
        NetScoreCalculator scoreCalculator { get; set; }
    }
}

