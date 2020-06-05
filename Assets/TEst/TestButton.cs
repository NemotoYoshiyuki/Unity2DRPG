using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEditor;
public class TestButton : MonoBehaviour
{
    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        StartCoroutine(Flash());
    }
    float sumTime = 0;
    float time = 3f;
    float b = 0f;

    public float minimum = -1.0F;
    public float maximum = 1.0F;

    // starting value for the Lerp
    static float t = 0.0f;
    // Update is called once per frame
    void Update()
    {

        // animate the position of the game object...
        //transform.position = new Vector3(Mathf.Lerp(minimum, maximum, time / t), 0, 0);

        ////// .. and increase the t interpolater
        //t += Time.deltaTime;
        //float a = Mathf.Lerp(0, 1, 1 / (time / t));
        //Debug.Log(a);
        //Debug.Log(t);

        //if (t >= time)
        //{
        //    EditorApplication.isPaused = true;
        //}
        //// now check if the interpolator has reached 1.0
        //// and swap maximum and minimum so game object moves
        //// in the opposite direction.
        //if (t > 1.0f)
        //{
        //    float temp = maximum;
        //    maximum = minimum;
        //    minimum = temp;
        //    t = 0.0f;
        //}
    }

    public void Onclick()
    {
        Debug.Log("click");
        Button button = gameObject.GetComponent<Button>();
        CanvasRenderer canvasRenderer = gameObject.GetComponent<CanvasRenderer>();
        Color color = canvasRenderer.GetColor();

        ColorBlock colorBlock = button.colors;
        colorBlock.disabledColor = color;
        button.colors = colorBlock;

        button.interactable = false;
    }

    //選択中点滅
    public IEnumerator Flash()
    {
        while (true)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.g, 0);
            yield return StartCoroutine(Fade(0.6f, 1f));
            //image.color = new Color(image.color.r, image.color.g, image.color.g, 0);
            yield return null;
            //yield break;
        }

        //image.aをもとの値に戻す

        yield break;
    }

    public Image image;
    //loop
    public IEnumerator Fade(float finalAlpha, float time)
    {
        //image.color = color;
        float elapsedTime = 0;
        float alpha = image.color.a;

        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            float a = Mathf.SmoothStep(0, finalAlpha, 1 / (time / elapsedTime));

            //0.6を超えると0に戻る
            //a = Mathf.Repeat(a, 0.6f);
            Debug.Log(1 / (time / elapsedTime));
            Debug.Log(a);
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return null;
        }
        Debug.Log(elapsedTime);
        Debug.Log("w");
        yield break;
    }
    //途中でホバーが解除された時の処理


    //} public IEnumerator Fade(float finalAlpha, float time)
    //{
    //    //image.color = color;
    //    float elapsedTime = 0;
    //    float alpha = image.color.a;

    //    while (elapsedTime <= time)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        alpha += Time.deltaTime / time;
    //        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    //        yield return null;
    //    }
    //    yield break;
    //}



    public float fadeDuration = 0.1f;
    public IEnumerator _Fade(float finalAlpha, ColorBlock canvasGroup)
    {
        Color color = Color.green;
        float fadeSpeed = Mathf.Abs(canvasGroup.normalColor.a - finalAlpha) / fadeDuration;
        Debug.Log(fadeSpeed);
        while (!Mathf.Approximately(canvasGroup.normalColor.a, finalAlpha))
        {
            float a = Mathf.MoveTowards(canvasGroup.normalColor.a, finalAlpha,
                   fadeSpeed * Time.deltaTime);
            color.a = a;
            //Debug.Log(fadeSpeed*Time.deltaTime);
            canvasGroup.normalColor = color;
            button.colors = canvasGroup;
            yield return null;
        }
        button.colors = canvasGroup;
        yield break;
    }
    //https://teratail.com/questions/158232
    //https://tama-lab.net/2017/07/unity%E3%81%A7%E3%83%95%E3%82%A7%E3%83%BC%E3%83%89%E3%82%A4%E3%83%B3%EF%BC%8F%E3%83%95%E3%82%A7%E3%83%BC%E3%83%89%E3%82%A2%E3%82%A6%E3%83%88%E3%82%92%E5%AE%9F%E8%A3%85%E3%81%99%E3%82%8B%E6%96%B9/


    //ホバー　緑点滅
    //クリック　緑固定
    //無効　緑固定

    //他のボタン
    //無効　通常色

    //fadeさせない
    //a = 0.6
    //a = 0
    //を繰り返す




    //最終決定
    //ホーバー時の点滅はボタンの色を使用
    //かさねない　セレクトカラーを使用して点滅
    //重ねるか
    //重ねない画面全体に効果　アニメーションでもスプライトでも使用できる
}
