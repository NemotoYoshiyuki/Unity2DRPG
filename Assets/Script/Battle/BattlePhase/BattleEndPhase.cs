using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndPhase : MonoBehaviour
{
    public IEnumerator Win()
    {
        SceneController.Instance.BackToField();
        yield break;
    }

    public IEnumerator GameOver()
    {
        yield return StartCoroutine(MessageSystem.GetWindow().ShowClick("まけました"));
        SceneController.Instance.Transition("Title");
        yield break;
    }
}
