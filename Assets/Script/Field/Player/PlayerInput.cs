using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://qiita.com/_tybt/items/bc9c3be75c04ab547c35
public class PlayerInput : MonoBehaviour
{
    public static Vector3 playerPotision;
    public static bool canMove = true;

    private Vector2 direction;
    bool isMoveing = false;
    private Camera mainCamara;

    [SerializeField]
    float SPEED = 1.0f;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;

    void Start()
    {
        // オブジェクトに設定しているRigidbody2Dの参照を取得する
        this.rigidBody = GetComponent<Rigidbody2D>();

        mainCamara = Camera.main;
    }

    void Update()
    {
        if (canMove == false) return;

        // x,ｙの入力値を得る
        // それぞれ+や-の値と入力の関連付けはInput Managerで設定されている
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        if (inputAxis != Vector2.zero) direction = inputAxis;
    }

    private void FixedUpdate()
    {
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * SPEED;

        //メインカメラをプレイヤーのPositionに代入する
        mainCamara.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);

        //プレイヤーの現在の座標を代入
        playerPotision = gameObject.transform.position;
    }

    public Vector2 GetDirection()
    {
        return direction.normalized;
    }

    public bool Moving()
    {
        return rigidBody.velocity.magnitude >= 0;
    }
}
