using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class SelectButton : MonoBehaviour
{
    public AudioClip click;
    private TextMeshProUGUI text;
    private AudioSource audioSource;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }
}
