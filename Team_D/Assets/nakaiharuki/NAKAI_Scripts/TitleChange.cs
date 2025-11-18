using UnityEngine;

public class TitleChange : MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("Title", 1.0f);
    }
    
}
