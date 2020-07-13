
using UnityEngine;
using UnityEngine.Experimental.Rendering;

//このオブジェクトにプレイヤーが触れたたらSceneTransitionDestinationを探してその場所に移動します
[RequireComponent(typeof(Collider2D))]
public class TransitionPoint : MonoBehaviour
{
    [Header("シーン移動先")]
    [SceneName] public string newSceneName;
    [Tooltip("The tag of the SceneTransitionDestination script in the scene being transitioned to.")]
    public SceneTransitionDestination.DestinationTag transitionDestinationTag;

    public bool canTransition = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTransition) return;
        canTransition = false;
        PlayerMovement.Instance.CancelMove();

        TransitionInternal();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canTransition = true;
    }

    protected void TransitionInternal()
    {
        PlayerTransitionController.Transition(newSceneName, transitionDestinationTag);
    }
}