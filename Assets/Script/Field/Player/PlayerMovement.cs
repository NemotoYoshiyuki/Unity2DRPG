using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//https://qiita.com/_tybt/items/bc9c3be75c04ab547c35
public class PlayerMovement : MonoBehaviour
{
    public static Vector3 playerPotision;
    public static bool canMove = true;
    public static Action onWalk;

    public float speed = 1.0f;

    private Animator animator;
    private Vector2 direction;
    private Camera mainCamara;
    private Rigidbody2D rigidBody;
    private Vector2 inputAxis;


    private void Awake()
    {
        // オブジェクトに設定しているRigidbody2Dの参照を取得する
        this.rigidBody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        //開始時に正面を向かせる
        UpdateAnimator(Vector2.down);

        this.mainCamara = Camera.main;
    }

    void Update()
    {
        if (canMove == false) return;

        // x,ｙの入力値を得る
        // それぞれ+や-の値と入力の関連付けはInput Managerで設定されている
        inputAxis.x = Input.GetAxis("Horizontal");
        inputAxis.y = Input.GetAxis("Vertical");

        if (inputAxis != Vector2.zero)
        {
            direction = inputAxis;
            UpdateAnimator(direction);
        }
    }

    private void FixedUpdate()
    {
        // 速度を代入する
        rigidBody.velocity = inputAxis.normalized * speed;

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

    private void UpdateAnimator(Vector2 vector)
    {
        this.animator.speed = 1.0f;
        this.animator.SetFloat("x", vector.x);
        this.animator.SetFloat("y", vector.y);
    }
}
