using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class test : MonoBehaviour
{
    public BattleVFX BattleVFX;
    public PlayableAsset playableAsset;

    void Start()
    {
        StartCoroutine(BattleVFX.Play(playableAsset));
    }
}
