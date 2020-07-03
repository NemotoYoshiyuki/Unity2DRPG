using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//プレイヤーの座標移動を管理します
public class PlayerTransitionController : MonoBehaviour
{
    [SerializeField] protected PlayerMovement m_PlayerInput;

    protected static PlayerTransitionController instance;
    public static PlayerTransitionController Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<PlayerTransitionController>();

            if (instance != null)
                return instance;

            GameObject gameObjectTeleporter = new GameObject("PlayerTransitionController");
            instance = gameObjectTeleporter.AddComponent<PlayerTransitionController>();

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

        if (m_PlayerInput == null)
        {
            m_PlayerInput = Resources.Load<PlayerMovement>("Player");
        }
    }

    public static void Transition(string newSceneName, SceneTransitionDestination.DestinationTag tag)
    {
        Instance.StartCoroutine(Instance.TransitionInternal(newSceneName, tag));
    }

    protected IEnumerator TransitionInternal(string newSceneName, SceneTransitionDestination.DestinationTag tag)
    {
        if (newSceneName != SceneManager.GetActiveScene().name)
        {
            yield return SceneManager.LoadSceneAsync(newSceneName);
        }

        PlayerMovement player = FindPlayerInput();
        SceneTransitionDestination sceneTransitionDestination = GetDestination(tag);
        TransitionPoint transitionPoint = sceneTransitionDestination.gameObject.GetComponent<TransitionPoint>();

        if (transitionPoint != null) transitionPoint.canTransition = false;
        player.transform.position = sceneTransitionDestination.transform.position;
        yield break;
    }

    protected SceneTransitionDestination GetDestination(SceneTransitionDestination.DestinationTag destinationTag)
    {
        SceneTransitionDestination[] entrances = FindObjectsOfType<SceneTransitionDestination>();
        for (int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }
        Debug.LogError("No entrance was found with the " + destinationTag + " tag.");
        return null;
    }

    public PlayerMovement FindPlayerInput()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player == null) player = Instantiate(m_PlayerInput);
        return player;
    }
}
