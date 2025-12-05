using UnityEngine;

public class StageChange :MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("Stage1", 1.0f);
    }
}
