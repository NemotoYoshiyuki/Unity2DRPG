using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationDirector : BattleDirector
{
    public AnimationClip animationClip;
    private PlayableAsset playableAsset;
    private Transform transform = null;

    public AnimationDirector(AnimationClip animationClip)
    {
        this.animationClip = animationClip;
    }

    public AnimationDirector(PlayableAsset playableAsset, Transform transform = null)
    {
        this.playableAsset = playableAsset;
        this.transform = transform;
    }

    public override IEnumerator Do()
    {
        Debug.Log("アニメーションを再生します");

        if (playableAsset == null)
        {
            yield break;
        }

        if (transform == null)
        {
            yield return BattleVFX.Instance.FullScreenPlay(playableAsset);
        }
        else
        {
            yield return BattleVFX.Instance.Play(playableAsset, transform);
        }

        yield break;
    }
}
