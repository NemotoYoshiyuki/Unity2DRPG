using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    public static Test test = new Test();
    public static Test test1 = new Test();
    // Start is called before the first frame update
    void Start()
    {
        test.a = 100;
        test.b = 200;

        test1.b = 1000;

        string json = JsonUtility.ToJson(test);
        string json1 = JsonUtility.ToJson(test1);
        Debug.Log(json);
        Debug.Log(json1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Test
{
    public int a;
    public int b;
}
