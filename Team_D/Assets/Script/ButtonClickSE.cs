using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSE : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlayButtonClickSE();
            }
        });
    }
}
