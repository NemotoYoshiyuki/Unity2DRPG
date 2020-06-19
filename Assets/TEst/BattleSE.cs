using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSE : MonoBehaviour
{
    public static BattleSE Instance;
    public AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Play(AudioClip audioClip)
    {
        Instance.audioSource.PlayOneShot(audioClip);
    }
}
