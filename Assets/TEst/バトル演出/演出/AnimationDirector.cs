using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationDirector : BattleDirector
{
    private PlayableAsset playableAsset;
    private Transform transform = null;

    public AnimationDirector(PlayableAsset playableAsset, Transform transform = null)
    {
        this.playableAsset = playableAsset;
        this.transform = transform;
    }

    public override IEnumerator Execute()
    {

        if (playableAsset == null) yield break;

        if (transform == null)
        {
            yield return BattleVFX.Instance.Play(playableAsset);
            yield break;
        }

        yield return BattleVFX.Instance.Play(playableAsset, transform);
        yield break;
    }
}
