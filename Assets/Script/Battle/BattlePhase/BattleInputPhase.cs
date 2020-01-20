using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInputPhase : MonoBehaviour
{
    public static bool finish = false;
    private BattleInputController battleInputController;

    private void Start()
    {
        battleInputController = BattleInputController.instance;
    }

    public IEnumerator Do()
    {
        battleInputController.Init();
        battleInputController.Change<BattleMainInput>();
        yield return new WaitUntil(() => finish);
        finish = false;
        yield break;
    }

    //BattleInputControllerからコマンド入力処理が終了したタイミンで呼び出されます
    public void Finish()
    {
        finish = true;
    }
}
