using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDirector : BattleDirector
{
    private AudioClip audioClip;

    public SoundDirector(AudioClip audioClip)
    {
        this.audioClip = audioClip;
    }

    public override IEnumerator Execute()
    {
        //効果音を再生します
        BattleSE.PlaySE(audioClip);
        yield break;
    }
}
