using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FPSCounter : MonoBehaviour
{
    public Text FpsText;

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % Application.targetFrameRate == 0)
        {
            FpsText.text = "FPS:" + (int)(1 / Time.deltaTime);
        }
    }
}