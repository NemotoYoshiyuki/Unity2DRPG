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

    public CommonEventExecution.LaunchCondition launchCondition;
    public ConditionalExecution conditional = new ConditionalExecution();
    public TextAsset LuaScript;

    //
    public LuaScript luaScript;


    private void Start()
    {
        //実行条件を満たしていないときGameObjectを非表示にします
        if (!IsExecute())
        {
            gameObject.SetActive(false);
            return;
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
        luaScript.Execution(LuaScript);
    }

    private bool IsExecute()
    {
        if (conditional.flagName == string.Empty) return true;

        return GameController.GetFlagManager().Comparison(conditional.flagName, conditional.flagValue);
    }
}