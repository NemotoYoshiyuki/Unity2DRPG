using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public Image image;
    private bool isStop = false;

    public void FlashStart(Color color, float time)
    {
        StartCoroutine(_Flash(color, time));
    }

    public void FlashLoopStart(Color color, float time)
    {
        StartCoroutine(_FlashLoop(color, time));
    }

    private IEnumerator _Flash(Color color, float time)
    {
        image.color = color;
        float elapsedTime = 0;
        float alpha = image.color.a;

        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            alpha -= Time.deltaTime / time;
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        yield break;
    }

    private IEnumerator _FlashLoop(Color color, float time)
    {
        IEnumerator flash = _Flash(color, time);
        while (isStop == false)
        {
            yield return StartCoroutine(flash);
        }
        yield break;
    }

    public void Stop()
    {
        isStop = true;
        image.color = Color.clear;
    }
}
