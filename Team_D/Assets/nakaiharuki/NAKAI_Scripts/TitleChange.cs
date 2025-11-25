using UnityEngine;

public class TitleChange : MonoBehaviour
{
    public void SwitchScene()
    {
        FadeManager.Instance.LoadScene("Title", 0.2f);
    }
    
}
