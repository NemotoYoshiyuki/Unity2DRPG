using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommonEventExecution : Interactable
{
    public enum LaunchCondition
    {
        決定キーで実行,
        自動実行,
        コライダー接触
    }

    [System.Serializable]
    public class ConditionalExecution
    {
        public string flagName;
        public bool flagValue;
    }


    [Header("起動条件")]
    public CommonEventExecution.LaunchCondition launchCondition;
    [Header("実行条件")]
    public ConditionalExecution conditional = new ConditionalExecution();
    [Header("実行するイベントスクリプト")]
    public TextAsset luaFile;


    private void Start()
    {
        //実行条件を満たしていないときGameObjectを非表示にします
        if (!IsExecute())
        {
            gameObject.SetActive(false);
            return;
        }

        if (launchCondition == LaunchCondition.コライダー接触)
        {
            if (GetComponent<BoxCollider2D>() == null) throw new System.Exception("Not BoxCollider2D");
        }

        if (launchCondition == LaunchCondition.自動実行)
        {
            Execute();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (launchCondition == LaunchCondition.コライダー接触)
        {
            Execute();
        }
    }

    public override void OnInteractable()
    {
        if (launchCondition == LaunchCondition.決定キーで実行)
        {
            Execute();
        }
    }


    private void Execute()
    {
        LuaScript.instance.Execution(luaFile);
    }

    private bool IsExecute()
    {
        if (conditional.flagName == string.Empty) return true;

        return GameController.GetFlagManager().Equals(conditional.flagName, conditional.flagValue);
    }
}