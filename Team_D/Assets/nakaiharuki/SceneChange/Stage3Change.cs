using UnityEngine;

public class Stage3Change : MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("Stage(3)", 1.0f);
    }
}
