using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDirectionExecutor : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hit;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Hit()
    {
        audioSource.PlayOneShot(hit);
    }

    public IEnumerator Miss()
    {
        yield break;
    }
}
