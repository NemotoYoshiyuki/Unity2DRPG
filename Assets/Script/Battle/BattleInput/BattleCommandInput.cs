using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleCommandInput : MonoBehaviour
{
  
    private void Awake()
    {
            
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void Open()
    {
        SetActive(true);
    }

    public virtual void Close()
    {
        SetActive(false);
    }

    public static BattleCommand GetWindow()
    {
        return null;
    }
}
