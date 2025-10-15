using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUnit : MonoBehaviour
{
    public GameObject Bullet; //弾の変数
    public float Speed;       //弾の速度

    private GameObject bulletIns;
    private Vector2 mousePos;
    private Vector2 angle;

    Vector3 BulletPoint; //弾の発射位置
    float timer;//タイマー
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BulletPoint = transform.Find("Bullet_Point").localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;//経過時間加算

        mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
            if (Input.GetMouseButtonDown(0) && timer > 1.0f)//左クリックで弾を発射
            {
            

                //弾の生成
                bulletIns = Instantiate(Bullet, transform.position + BulletPoint, Quaternion.identity);
                Vector2 angle = (mousePos - (Vector2)transform.position).normalized;
                bulletIns.GetComponent<Rigidbody2D>().linearVelocity = angle * Speed;

                Destroy(bulletIns, 1.5f);    //一定時間経過で弾削除
                timer = 0;                   //タイマーリセット
            }
        
    }
}
