using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartPhase : MonoBehaviour
{
    public IEnumerator Do()
    {
        //暗転を解除
        yield return StartCoroutine(SceneFader.FadeSceneIn());

        //遭遇メッセージを表示
        yield return StartCoroutine(MessageSystem.GetWindow().ShowAuto(BattleText.Get("BattleStart")));

        yield break;
    }
}
