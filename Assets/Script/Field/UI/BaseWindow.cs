using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWindow : MonoBehaviour
{
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }

    public virtual void Cancel()
    {

    }
}
