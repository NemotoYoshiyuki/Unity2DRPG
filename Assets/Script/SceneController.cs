using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static string currentScene;
    public string CurrentScene => SceneManager.GetActiveScene().name;

    protected static SceneController instance;
    public static SceneController Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<SceneController>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static SceneController Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<SceneController>();

        return instance;
    }


    public void Transition(string newSceneName)
    {
        currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(newSceneName);
    }

    public void Transition(string newSceneName, Vector2 playerPosition)
    {
        StartCoroutine(TransitionInternal(newSceneName, playerPosition));
    }

    public IEnumerator TransitionInternal(string newSceneName, Vector2 playerPosition)
    {
        yield return SceneManager.LoadSceneAsync(newSceneName);
        PlayerMovement playerInput = PlayerTransitionController.Instance.FindPlayerInput();
        playerInput.transform.position = playerPosition;
        yield break;
    }

    public IEnumerator BackToField()
    {
        //バトルシーンからフィールドシーンに戻ります
        yield return StartCoroutine(instance.TransitionToField());
        yield break;
    }

    //バトルシーンからフィールドシーンに戻ったときにイベントを開始します
    public IEnumerator BackToField(TextAsset luaFile)
    {
        //バトルシーンからフィールドシーンに戻ります
        yield return StartCoroutine(instance.TransitionToField());
        //イベントを開始します
        LuaScript.instance.luaFile = luaFile;
        StartCoroutine(LuaScript.instance.RunCoroutine());
        yield break;
    }

    private IEnumerator TransitionToField()
    {
        yield return SceneManager.LoadSceneAsync(currentScene);
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.gameObject.transform.position = PlayerMovement.playerPotision;
        yield break;
    }
}
