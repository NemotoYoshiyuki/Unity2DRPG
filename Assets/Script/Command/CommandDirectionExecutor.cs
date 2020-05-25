using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDirectionExecutor : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hit;//技によって変わるけど意味ないよ　打撃？斬撃？銃撃？火炎？
    public AudioClip miss;
    public AudioClip 回避;
    public AudioClip 会心;
    public AudioClip 痛恨;
    public AudioClip 逃走;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Hit()
    {
        //アニメーション　打撃？斬撃？銃撃？火炎？
        audioSource.PlayOneShot(hit);
    }

    public IEnumerator Miss()
    {
        yield break;
    }

    public class PlaySE
    {
        public void HitSound()
        {
           
        }

        public void MissSound()
        {

        }
    }
}
