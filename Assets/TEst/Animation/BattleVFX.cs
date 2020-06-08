using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class BattleVFX : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector vfx;
    public static BattleVFX Instance;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Play(PlayableAsset playableAsset, Transform position)
    {
        PlayableDirector VFX = Instantiate(vfx, position);
        VFX.playableAsset = playableAsset;
        VFX.gameObject.transform.localScale = Vector3Int.one * 2;
        VFX.Play();
        
        //再生終了
        while (VFX.state != PlayState.Paused)
        {
            yield return null;
        }

        Destroy(VFX.gameObject);
        yield break;
    }

    public IEnumerator FullScreenPlay(PlayableAsset playableAsset)
    {
        PlayableDirector VFX = Instantiate(vfx);
        VFX.playableAsset = playableAsset;
        VFX.gameObject.transform.localScale = Vector3Int.one * 4;
        VFX.Play();

        //再生終了
        while (VFX.state != PlayState.Paused)
        {
            yield return null;
        }

        Destroy(VFX.gameObject);
        yield break;
    }
}
