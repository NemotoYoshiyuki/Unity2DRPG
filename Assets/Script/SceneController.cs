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
        PlayerInput playerInput = PlayerTransitionController.Instance.FindPlayerInput();
        playerInput.transform.position = playerPosition;
        yield break;
    }

    public void BackToField()
    {
        //バトルシーンからフィールドシーンに戻ります
        StartCoroutine(instance.TransitionToField());
    }

    private IEnumerator TransitionToField()
    {
        yield return SceneManager.LoadSceneAsync(currentScene);
        PlayerInput player = FindObjectOfType<PlayerInput>();
        player.gameObject.transform.position = PlayerInput.playerPotision;
        yield break;
    }
}
