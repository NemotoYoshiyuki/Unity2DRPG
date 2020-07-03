using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndPhase : MonoBehaviour
{
    public IEnumerator Win()
    {
        BattleController.isBattle = false;
        yield return StartCoroutine(SceneController.BackToField());
        yield break;
    }

    public IEnumerator GameOver()
    {
        BattleController.isBattle = false;
        yield return StartCoroutine(BattleMessage.GetWindow().ShowClick("まけました"));
        //SceneController.Instance.Transition("Title");
        GameController.Instance.GameOver();
        yield break;
    }
}
