using UnityEngine;
using UnityEngine.UI;


public class PlayerHP : MonoBehaviour
{
   
    public GameObject HPIcon; //HPアイコンのプレハブ

    private PlayerContller Player; //プレイヤーコントローラーのスクリプトを取得
    private int BeforeHP;          //前回のHPを記録


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = FindObjectOfType<PlayerContller>();
        BeforeHP = Player.GetHP();
        CreateHPIcon();
    }

    private void CreateHPIcon()
    {
        //プレイヤーのHPの数だけアイコンを生成
        for(int i = 0; i<Player.GetHP(); i++)
        {
            GameObject PlayerHPObj = Instantiate(HPIcon);
            PlayerHPObj.transform.SetParent(transform);  //親（HP）オブジェクトに設定
        }
    }

    // Update is called once per frame
    void Update()
    {
        ShowHPIcon(); //HPの変化をチェック
    }

    private void ShowHPIcon()
    {
        //HPが変わっていなければ何もしない
        if (BeforeHP == Player.GetHP()) return;

        //すべてのHPアイコンを取得
        Image[] Icons = transform.GetComponentsInChildren<Image>();

        //現在のHP以下のアイコンのみ表示、それ以外は非表示
        for(int i = 0; i<Icons.Length; i++)
        {
            Icons[i].gameObject.SetActive(i < Player.GetHP());
        }

        BeforeHP = Player.GetHP();
    }
}
