using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class BattleVFX : MonoBehaviour
{
    public static BattleVFX Instance;
    [SerializeField] private PlayableDirector VFX;

    private void Awake()
    {
        Instance = this;
    }

    //指定した位置で小さく再生
    public IEnumerator Play(PlayableAsset playableAsset, Transform position)
    {
        PlayableDirector m_VFX = Instantiate(VFX, position);
        m_VFX.playableAsset = playableAsset;
        m_VFX.gameObject.transform.localScale = Vector3Int.one * 2;
        Bind(m_VFX);
        m_VFX.Play();

        //再生終了待機
        while (m_VFX.state != PlayState.Paused)
        {
            yield return null;
        }
        Destroy(m_VFX.gameObject);
        yield break;
    }

    //画面中央で大きく再生
    public IEnumerator Play(PlayableAsset playableAsset)
    {
        PlayableDirector m_VFX = Instantiate(VFX);
        m_VFX.playableAsset = playableAsset;
        m_VFX.gameObject.transform.localScale = Vector3Int.one * 4;
        Bind(m_VFX);
        m_VFX.Play();

        //再生終了待機
        while (m_VFX.state != PlayState.Paused)
        {
            yield return null;
        }

        Destroy(m_VFX.gameObject);
        yield break;
    }

    private void Bind(PlayableDirector VFX)
    {
        Animator animator = VFX.GetComponent<Animator>();
        var animationBinding = VFX.playableAsset.outputs.First(c => c.streamName == "Animation Track");
        VFX.SetGenericBinding(animationBinding.sourceObject, animator);
        Debug.Log(animationBinding);

        AudioSource audioSource = VFX.GetComponent<AudioSource>();
        var audioSourceBinding = VFX.playableAsset.outputs.First(c => c.streamName == "Audio Track");
        VFX.SetGenericBinding(audioSourceBinding.sourceObject, animator);
    }
}
