using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using TMPro;
using UnityEditor;

public class SelectItem : Button
{
    public int index;
    public Action onHover;
    public Action onHoverExit;

    public float duration = 2f;
    public float finalAlpha = 0.6f;

    private ClickEvent m_clickEvent;
    private bool m_clickable = true;
    private bool m_focus = false;
    private TextMeshProUGUI textMeshProUGUI;
    private IEnumerator flashLoop;

    public class ClickEvent : UnityEvent<int> //引数の型を指定しておく
    {

    }

    public ClickEvent clickEvent
    {
        get
        {
            if (m_clickEvent == null)
            {
                m_clickEvent = new ClickEvent();
            }
            return m_clickEvent;
        }
    }

    public string text
    {
        set
        {
            if (textMeshProUGUI == null)
            {
                textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
            }

            textMeshProUGUI.SetText(value);
        }
    }

    public bool clickable
    {
        get
        {
            return m_clickable;
        }
        set
        {
            m_clickable = value;
            interactable = value;

            if (m_clickable != false) return;

            ColorBlock colorBlock = this.colors;

            if (currentSelectionState == SelectionState.Selected)
            {
                colorBlock.disabledColor = colors.selectedColor;
            }
            else
            {
                colorBlock.disabledColor = colors.normalColor;
            }

            colors = colorBlock;

        }
    }

    public bool focus
    {
        get
        {
            return m_focus;
        }
        set
        {
            m_focus = value;

            if (m_focus)
            {
                StartFlash();
            }
            else if (flashLoop != null)
            {
                StopFlash();
            }
        }
    }

    public void SetUp(int index)
    {
        this.index = index;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (onHover != null) onHover.Invoke();

        if (clickable == false) return;
        if (transition != Transition.ColorTint) return;
        StartFlash();
        //Debug.Log("Enter");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (onHoverExit != null) onHoverExit.Invoke();

        if (clickable == false) return;
        if (transition != Transition.ColorTint) return;
        StopFlash();
        //Debug.Log("Exit");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        clickEvent?.Invoke(index);

        if (transition != Transition.ColorTint) return;
        StopFlash();
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        if (interactable == false) return;
        Debug.Log("OnSelect");
        StartFlash();
    }

    public void ChangeOpacity(float alpha)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }

    private IEnumerator FlashLoop(float finalAlpha, float duration)
    {
        //waitを挿入
        yield return new WaitForEndOfFrame();

        float elapsedTime = 0;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

        while (true)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, finalAlpha, 1 / (duration / elapsedTime));

            alpha = Mathf.Repeat(alpha, finalAlpha);
            elapsedTime = Mathf.Repeat(elapsedTime, duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

            yield return null;
        }
    }

    public void StartFlash()
    {
        //点滅開始
        flashLoop = FlashLoop(finalAlpha, duration);
        StartCoroutine(flashLoop);
        Debug.Log("flash");
    }

    public void StopFlash()
    {
        //点滅終了
        StopCoroutine(flashLoop);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);

        flashLoop = null;
    }
}

//アイテムコントロール
//ボタンを作成する
//ボタンを表示する

//アイテムボタン
/*
 * 登録する
 * キャラはここをいじって複数イベントに対応？
 * public event EventHandler Click;
 * this.button1.Click+=new System.EventHandler(this.button1_Click);
 * 
 * 登録される
 * 第一引数はイベントを引き起こしたインスタンス
 * ItemWindow等が該当するのか？
 * OnClick(SelectButton sender,EventArgs eventArgs){
 *  e.item
 *  e.target
 *  なにか処理をする
 * }
 * 
 * OnPointerClick{
 * ハンドラーを実行
 * Click.Invoke();
 * }
 */

//これだとキャラボタンは？
//バトルコマンドに流用できそう？
//匿名関数は引数の都合で難しい
//イベントハンドラ削除がやっかい

//各Windowをパネルからキャンバスに変更
//複数選択　全選択はボタン入力を受け付けない
//項目リストクラス

/*
 * ０層
 * メニューコントローラー　各ウィンドウの親　全体の管理
 * １層
 * サイドメニュー　２層を呼び出す
 * キャラメニュー　ステータスの表示　ターゲット決定ボタンを兼ねる
 * ２層
 * アイテムメニュー　アイテムリスト表示　アイテム説明表示　アイテム動作実行(使用・捨てる)
 * スペルメニュー　上に同じ
 * 装備メニュー　未実装
 */

//ナビゲーション操作で誤算
//とりあえずこれを終わらせてgitにあげる
//その後に色々作業に取り掛かる

//https://narumincho.com/ui-color UIの配色の参考になる
//暗転させて選択を強調する
//カーソル移動音　ページ送り音
//エンカウントエリアによってエンカウント率を変える　森なら出やすい　ギミックが多いなら出にくい

//ナビゲーションはUI.Selectbleを継承する必要がある
//ナビゲーションはinteractableのときには使えない
//ナビゲーションはハイライトが使えない
//Invalid 無効
//使用できないな時は文字を薄く　アイコンも薄く　インターフェイス？
//無効のボタンを押した時ブザー音　ホバーは無効にしない
//カーソル移動音　ホーバーでなる
//セレクト音　決定で鳴る
//ナービゲーションでオンホバーが効かなない
//ただしOnSelectは機能している
//連続入力対策
//indexは外に公開してもいい　あらかじめシーン上に配置する場合
//ベースカラーは黒、メインカラーはグレー、アクセントカラーは薄緑です。

/*
 * // ボタンのクリックを受け付けるかどうか
    private bool _clickable = true;

    //マルチタップの防止
    Input.multiTouchEnabled = false;
 */

//最終仕様
/*
 * Layout
 * 
 * Input
 * 入力に対するSEの再生
 * 
 * Action
 * 
 * 仕様不可時の動作
 * interactableは使わない
 * クリック不可
 * ホバー有効
 * ナビゲーションの対象
 * アイコン・文字を薄く
 * ハイライトを通常よりも薄く
 * クリックした時ブザー音を鳴らす
 * 
 * Constraints 制約
 * interactableは使わない
 * 連続入力不可　ディレイをかける delta	最後の更新からのデルタポインタ
 * マルチタップ不可
 */

/*
 * 色の維持
 * Color color = selectable.GetCurrentColor();
      selectable.interactable = false;
      selectable.ChangeDisabledColor(color);
 */
