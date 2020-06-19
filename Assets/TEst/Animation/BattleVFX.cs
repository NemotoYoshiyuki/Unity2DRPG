using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class BattleVFX : MonoBehaviour
{
    public PlayableDirector VFX;
    public static BattleVFX Instance;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Play(PlayableAsset playableAsset, Transform position)
    {
        PlayableDirector m_VFX = Instantiate(VFX, position);
        m_VFX.playableAsset = playableAsset;
        m_VFX.gameObject.transform.localScale = Vector3Int.one * 2;
        m_VFX.Play();

        //再生終了
        while (m_VFX.state != PlayState.Paused)
        {
            yield return null;
        }

        Destroy(m_VFX.gameObject);
        yield break;
    }

    public IEnumerator Play(PlayableAsset playableAsset)
    {
        PlayableDirector m_VFX = Instantiate(VFX);
        m_VFX.playableAsset = playableAsset;
        m_VFX.gameObject.transform.localScale = Vector3Int.one * 4;
        m_VFX.Play();

        //再生終了
        while (m_VFX.state != PlayState.Paused)
        {
            yield return null;
        }

        Destroy(m_VFX.gameObject);
        yield break;
    }
}
