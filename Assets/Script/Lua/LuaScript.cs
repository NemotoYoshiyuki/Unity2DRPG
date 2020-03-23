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
    protected DynValue coroutine;
    protected string luaCode;

    public static LuaScript Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<LuaScript>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    private static void Create()
    {
        GameObject gameObject = new GameObject("LuaScript");
        instance = gameObject.AddComponent<LuaScript>();
        instance.luaEvent = gameObject.AddComponent<LuaEventScript>();
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        Initialized();
        InitModule();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Execution(TextAsset luaFile)
    {
        this.luaFile = luaFile;
        luaCode = luaFile.text;
        StartCoroutine(RunCoroutine());
    }

    public void Execution(string code)
    {
        luaCode = code;
        StartCoroutine(RunCoroutine());
    }

    private void Initialized()
    {
        UserData.RegisterAssembly(typeof(LuaScript).Assembly);
        UserData.RegisterType<PlayerCharacter>();
        UserData.RegisterType<Status>();

        script = new Script(CoreModules.Preset_Complete);
        object[] result = Resources.LoadAll("Lua", typeof(TextAsset));
        script.Options.ScriptLoader = new LuaScriptLoader(result.OfType<TextAsset>());
    }

    private void InitModule()
    {
        DynValue value = script.RequireModule("LuaFunction");
        Table fungusTable = null;
        fungusTable = value.Function.Call().Table;
        if (fungusTable == null) UnityEngine.Debug.LogError("Failed to create Fungus table");
        script.Globals["LuaFunction"] = fungusTable;

        //C#
        fungusTable["luaEvent"] = luaEvent;

        //関数を登録する
        foreach (TablePair p in fungusTable.Pairs)
        {
            if (script.Globals.Keys.Contains(p.Key))
            {
                UnityEngine.Debug.LogError("Lua globals already contains a variable " + p.Key);
            }
            else
            {
                //Debug.Log(p.Key + "  " + p.Value);
                script.Globals[p.Key] = p.Value;
            }
        }
    }

    public IEnumerator RunCoroutine()
    {
        //プレイヤーの移動を制御
        PlayerInput.canMove = false;
        PlayerInteract.canInteract = false;

        //コルーチンを開始
        Run();

        //Luaコルーチンの終了を監視
        while (coroutine.Coroutine.State != MoonSharp.Interpreter.CoroutineState.Dead)
        {
            yield return null;
        }

        //メッセージウィンドウを閉じる等の終了処理
        PlayerInput.canMove = true;
        PlayerInteract.canInteract = true;
        luaEvent.End();
        yield break;
    }

    public void Run()
    {
        // コードをコンパイル
        DynValue function = script.DoString(luaCode);
        // コルーチンを生成
        coroutine = script.CreateCoroutine(function);
        // コルーチン開始
        coroutine.Coroutine.Resume();
    }

    public void Resume()
    {
        //コルーチンを再開
        coroutine.Coroutine.Resume();
    }
}
