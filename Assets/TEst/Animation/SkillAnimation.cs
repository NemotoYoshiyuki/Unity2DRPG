using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkillAnimation : MonoBehaviour
{
    public PlayableDirector playableDirector;

    //Asset
    public PlayableAsset skill0;
    public PlayableAsset skill1;

    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();

        playableDirector.stopped += OnPlayableDirectorStopped;

        playableDirector.playableAsset = skill1;
        playableDirector.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(PlayableAsset playableAsset)
    {
        playableDirector.playableAsset = playableAsset;
        playableDirector.Play();
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (playableDirector == aDirector)
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
        gameObject.SetActive(false);
    }
}
