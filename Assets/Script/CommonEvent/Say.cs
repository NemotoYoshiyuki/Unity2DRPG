using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Say : CommonEvent
{
    public IEnumerator Do()
    {
        StartCoroutine(Do());
        yield break;
    }
}
