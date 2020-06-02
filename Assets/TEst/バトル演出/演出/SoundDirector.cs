using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDirector : BattleDirector
{
    public AudioClip audioClip;

    public SoundDirector(AudioClip audioClip)
    {
        this.audioClip = audioClip;
    }

    public override IEnumerator Do()
    {
        //効果音を再生します

        yield break;
    }
}
