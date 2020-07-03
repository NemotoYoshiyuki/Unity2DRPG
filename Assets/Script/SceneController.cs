using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string previousScene;
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

    public static void Transition(string newSceneName)
    {
        Instance.previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(newSceneName);
    }

    public static void Transition(string newSceneName, Vector2 playerPosition)
    {
        Instance.StartCoroutine(TransitionInternal(newSceneName, playerPosition));
    }

    public static IEnumerator BackToField()
    {
        //バトルシーンからフィールドシーンに戻ります
        yield return Instance.StartCoroutine(TransitionInternal(Instance.previousScene, PlayerMovement.playerPotision));
        yield break;
    }

    static IEnumerator TransitionInternal(string newSceneName, Vector2 playerPosition)
    {
        yield return SceneManager.LoadSceneAsync(newSceneName);
        PlayerMovement playerInput = PlayerTransitionController.Instance.FindPlayerInput();
        playerInput.transform.position = playerPosition;
        yield break;
    }
}