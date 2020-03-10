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
        Initialized();
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
        script.Globals["evt"] = luaEvent;
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
        yield break;
    }

    public void Run()
    {
        // コードをコンパイル
       // DynValue function = script.DoString(luaFile.text);
        DynValue function = script.DoString(GetLuaCode());
        //// コルーチンを生成
        DynValue coroutine = script.CreateCoroutine(function);
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
            lua = require('LuaFunction')
            return function()
            lua.say('say')
            end
            ";

        return code;
    }
}
