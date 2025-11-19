using UnityEngine;

public class StageSelectChenge : MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("StageSelect", 0.5f);
    }
}

