using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Option : MonoBehaviour
{
    [Range(0, 1)] public float messageSpeed;
    [Range(0, 1)] public float soundVolume;
    [Range(0, 1)] public float sfxVolume;

    public Action ChangeVolume;
}
