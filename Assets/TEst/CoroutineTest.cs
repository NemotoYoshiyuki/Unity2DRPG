using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Coroutine coroutine = new Coroutine();
        StartCoroutine(coroutine.Do());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Coroutine
{
    public IEnumerator Do()
    {
        Debug.Log("A");
        yield return new WaitForSeconds(1f);
        Debug.Log("B");
        yield return Enumerator();
        Debug.Log("D");
        yield break;
    }

    public IEnumerator Enumerator()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("C");
        yield break;
    }
}