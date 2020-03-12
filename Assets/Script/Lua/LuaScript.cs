using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using MoonSharp.Interpreter;

public class LuaScript : MonoBehaviour
{
    public static LuaScript instance;
    public LuaEventScript luaEvent;

    public TextAsset luaFile;
    protected Script script;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Initialized();
        InitFungusModule();
        //
        Run();
    }

    public void Execution(TextAsset luaFile)
    {
        this.luaFile = luaFile;
        Run();
    }

    private void Initialized()
    {
        UserData.RegisterAssembly(typeof(LuaScript).Assembly);

        script = new Script(CoreModules.Preset_Complete);
        //script.Globals["evt"] = luaEvent;
        object[] result = Resources.LoadAll("Lua", typeof(TextAsset));
        script.Options.ScriptLoader = new LuaScriptLoader(result.OfType<TextAsset>());
    }

    protected DynValue coroutine;
    public IEnumerator RunCoroutine()
    {
        //プレイヤーの移動を制御

        while (coroutine.Coroutine.State != MoonSharp.Interpreter.CoroutineState.Dead)
        {
            //Sayコルーチンが終了するまで待機
            yield return null;

            // スクリプトを再開
            //coroutine.Coroutine.Resume();
        }

        //Luaスクリプトが終了した
        //メッセージウィンドウを閉じる等の終了処理
        yield break;
    }

    public void Run()
    {
        // コードをコンパイル
        // DynValue function = script.DoString(luaFile.text);
        DynValue function = script.DoString(GetLuaCode());
        //// コルーチンを生成
        //DynValue coroutine = script.CreateCoroutine(function);
        coroutine = script.CreateCoroutine(function);
        //// コルーチン開始
        coroutine.Coroutine.Resume();
    }

    public void Resume()
    {
        //コルーチンを再開
        coroutine.Coroutine.Resume();
    }

    //テストスペース
    public string GetLuaCode()
    {
        string code =
            @"-- Load the 'junglestory' module
--a = require('LuaFunction')
            f = require('NPC')
return function()
say('ll')
            f.start()
--a.say('p')
           end
            ";

        //return code;
        return luaFile.text;
    }
    protected LuaEventScript luaEnvironment { get; set; }
    public void InitFungusModule()
    {
        luaEnvironment = luaEvent;
        DynValue value = script.RequireModule("LuaFunction");
        Table fungusTable = null;
        fungusTable = value.Function.Call().Table;
        if (fungusTable == null) UnityEngine.Debug.LogError("Failed to create Fungus table");
        script.Globals["LuaFunction"] = fungusTable;

        //
        Debug.Log(luaEvent);
        fungusTable["luaEvent"] = luaEnvironment;

        foreach (TablePair p in fungusTable.Pairs)
        {
            if (script.Globals.Keys.Contains(p.Key))
            {
                UnityEngine.Debug.LogError("Lua globals already contains a variable " + p.Key);
            }
            else
            {
                script.Globals[p.Key] = p.Value;
            }
        }
    }
}
