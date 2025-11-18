using UnityEngine;

public class Stage2Change : MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("Stage2", 1.0f);
    }
}

