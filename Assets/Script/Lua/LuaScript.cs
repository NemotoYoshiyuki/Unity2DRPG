using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using MoonSharp.Interpreter;

public class LuaScript : MonoBehaviour
{
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
        object[] result = Resources.LoadAll("Lua", typeof(TextAsset));
        script.Options.ScriptLoader = new LuaScriptLoader(result.OfType<TextAsset>());
    }

    public IEnumerator RunCoroutine()
    {
        yield break;
    }

    public void Run()
    {
        // コードをコンパイル
        DynValue function = script.DoString(luaFile.text);
        //// コルーチンを生成
        DynValue coroutine = script.CreateCoroutine(function);
        //// コルーチン開始
        coroutine.Coroutine.Resume();
    }
}
