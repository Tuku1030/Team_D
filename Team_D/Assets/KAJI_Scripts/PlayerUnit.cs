using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUnit : MonoBehaviour
{
    public GameObject BigBullet; //巨大網（弾）の変数
    public GameObject Bullet;    //小網（弾）の変数
    public float Speed;          //弾の速度

    
    private GameObject BulletIns;
    private GameObject BigBulletIns;
    private Vector2 MousePos;
    private Vector2 Angle;

    Vector3 BigBulletPoint; //巨大網の（弾）発射位置
    Vector3 BulletPoint;    //小網（弾）の発射位置
    float Timer;//小網タイマー
    float BigTimer;//巨大網タイマー
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BulletPoint = transform.Find("Bullet_Point").localPosition;
        BigBulletPoint = transform.Find("BigBullet_Point").localPosition;

       
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;    //経過時間加算
        BigTimer += Time.deltaTime; //経過時間加算

        MousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

       
        if (Input.GetMouseButtonDown(0) && Timer > 1.0f)//左クリックで弾を発射
        {
            

                //弾の生成
                BulletIns = Instantiate(Bullet, transform.position + BulletPoint, Quaternion.identity);
                Vector2 Angle = (MousePos - (Vector2)transform.position).normalized;
                BulletIns.GetComponent<Rigidbody2D>().linearVelocity = Angle * Speed;

            

            Destroy(BulletIns, 1.5f);    //一定時間経過で弾削除
                Timer = 0; ;              //タイマーリセット
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && BigTimer > 5.0f)
        {
           

            BigBulletIns = Instantiate(BigBullet,transform.position + BigBulletPoint, Quaternion.identity);
            Destroy(BigBulletIns, 0.2f); //一定時間経過で弾削除
            BigTimer = 0;                //タイマーリセット
        }
    }
}
