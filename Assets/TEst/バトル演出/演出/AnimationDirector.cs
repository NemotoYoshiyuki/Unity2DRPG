using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDirector : BattleDirector
{
    public AnimationClip animationClip;

    public AnimationDirector(AnimationClip animationClip)
    {
        this.animationClip = animationClip;
    }

    public override IEnumerator Do()
    {
        Debug.Log("アニメーションを再生します");

        yield break;
    }
}
