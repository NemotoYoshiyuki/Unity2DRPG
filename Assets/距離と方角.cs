using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 距離と方角 : MonoBehaviour
{
    public Vector3 A = new Vector3();
    public Vector3 B = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        //距離
        float norm = (A - B).magnitude;
        //方角
        Vector3 v = (B - A).normalized;

        Debug.Log("距離は" + norm);
        Debug.Log("方角は" + v);
        Debug.Log("Dotは"+Vector3.Dot(A, B));
    }
}
