﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//https://qiita.com/_tybt/items/bc9c3be75c04ab547c35
public class PlayerMovement : MonoBehaviour
{
    public static Vector3 playerPotision;
    public static bool canMove = true;
    public static Action onWalk = null;

    public float Speed = 10f;
    public float movingdistance = 0.5f;

    private Animator animator;
    private Vector2 direction;
    private Camera mainCamara;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Vector2 inputAxis;
    private bool moveing = false;


    private void Awake()
    {
        // オブジェクトに設定しているRigidbody2Dの参照を取得する
        this.rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamara = Camera.main;
    }

    void Start()
    {
        //開始時に正面を向かせる
        UpdateAnimator(Vector2.down);
    }

    void Update()
    {
        mainCamara.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);

        if (canMove == false) return;
        if (moveing == true) return;

        // // x,ｙの入力値を得る
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");

        if (inputAxis != Vector2.zero)
        {
            //斜め入力は受け付けない
            if (inputAxis.x != 0 && inputAxis.y != 0) return;

            if (direction != inputAxis)
            {
                direction = inputAxis;
                UpdateAnimator(direction);
            }

            bool canWalk = Move((int)inputAxis.x, (int)inputAxis.y);
            if (canWalk)
            {
                playerPotision = gameObject.transform.position;
                onWalk?.Invoke();
            }
        }
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

    //目的地に向かって線形移動を行う
    private IEnumerator SmoothMovement(Vector3 end)
    {
        moveing = true;
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (transform.position != end)
        //while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPostion = Vector3.MoveTowards(rigidBody.position, end, Speed * Time.deltaTime);

            rigidBody.MovePosition(newPostion);

            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
        moveing = false;
        yield break;
    }

    public bool Move(int xDir, int yDir)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir * movingdistance, yDir * movingdistance);

        //boxCollider.enabled = false;
        //x方向は少し下にRayを飛ばす
        Vector2 v = new Vector2(0, 0.1f);
        //自分と同一のレイヤーとEncountArea以外のすべてを判定する
        LayerMask mask = ~(1 << gameObject.layer | 1 << LayerMask.NameToLayer("EncountArea"));
        RaycastHit2D hit = Physics2D.Linecast(start - v, end - v, mask);
        Debug.DrawLine(start - v, end - v, Color.blue, 1f);
        //boxCollider.enabled = true;

        //エンカウントエリアを取ってしまう
        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        else if (hit.collider.isTrigger == true)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }
}