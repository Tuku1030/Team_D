using UnityEngine;

public class OptionChange : MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("Option", 1.0f);
    }
}
