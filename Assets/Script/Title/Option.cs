using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Option : MonoBehaviour
{
    [Range(0,5)]public int messageSpeed;
    [Range(0, 5)] public int soundVolume;
    [Range(0, 5)] public int sfxVolume;

    public Action ChangeVolume;
}
