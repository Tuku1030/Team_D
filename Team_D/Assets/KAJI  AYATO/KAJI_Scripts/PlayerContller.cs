using UnityEngine;
using UnityEngine.UIElements;


public class PlayerContller : MonoBehaviour
{
    public float Speed = 0.005f;   //プレイヤーのスピード
    Rigidbody2D Rbody; //Rigidbody2Dの変数
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rbody = GetComponent<Rigidbody2D>(); //Rigidbody2Dを取得
    }

    // Update is called once per frame
    void Update()
    {
      //プレイヤーの座標
      Vector2 position = transform.position;

        //移動処理
        if(Input.GetKey(KeyCode.A))   //左
        {
            position.x -= Speed;
        }
        if (Input.GetKey(KeyCode.D)) //右
        {
            position.x += Speed;
        }
        if (Input.GetKey(KeyCode.W))    //上
        {
            position.y += Speed;
        }
        if (Input.GetKey(KeyCode.S))  //下
        {
            position.y -= Speed;
        }
        transform.position = position;

        //画面外に出ないようにする
        transform.position = new Vector2(
          //エリア指定して移動する
          Mathf.Clamp(transform.position.x,  -17.6f, 3.0f),
          Mathf.Clamp(transform.position.y,  -3.8f, 4.5f)
          );

    }

       
    
    private void FixedUpdate()
    {
        ////
        //float HorizontalInput = Input.GetAxis("Horizontal");
        //float VerticalInput = Input.GetAxis("Vertical");

        ////
        //Vector2 Movement = new Vector2(HorizontalInput, VerticalInput) * Speed;


        //Rbody.linearVelocity = Movement;
    }
}
