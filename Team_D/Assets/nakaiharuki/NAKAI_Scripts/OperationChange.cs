using UnityEngine;

public class Opere : MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("Operation", 1.0f);
    }
}
