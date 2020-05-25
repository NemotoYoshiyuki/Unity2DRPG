using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleController : MonoBehaviour
{
    public List<PlayerCharacter> playerCharacters;
    public List<EnemyCharacter> enemyCharacters;

    private bool escape = false;

    //バトルフェイズ
    public BattleSetUpPhase battleSetUp;
    public BattleStartPhase battleStart;
    public BattleInputPhase battleInput;
    public BattleCombatPhase battleCombat;
    public BattleResultPhase battleResult;
    public BattleEndPhase battleEnd;

    public static BattleController instance;
    public static bool isBattle;

    private void Awake()
    {
        instance = this;
        isBattle = true;
    }

    public List<PlayerCharacter> GetPlayerCharacters()
    {
        return playerCharacters;
    }

    public List<EnemyCharacter> GetEnemyCharacters()
    {
        return enemyCharacters;
    }

    public List<PlayerCharacter> AlivePlayerCharacters => playerCharacters.Where(x => !x.IsDead()).ToList();
    public List<EnemyCharacter> AliveEnemyCharacters => enemyCharacters.Where(x => !x.IsDead()).ToList();


    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    public IEnumerator GameLoop()
    {
        yield return StartCoroutine(battleSetUp.SetUp());
        yield return StartCoroutine(battleStart.Do());


        //勝敗がつくまでループする
        while (!Settle())
        {
            //コマンド入力
            yield return StartCoroutine(battleInput.Do());

            //逃亡
            if (escape) yield return StartCoroutine(Escape());

            //戦闘
            yield return StartCoroutine(battleCombat.Combat());
        }

        //敗北した時
        if (IsLose())
        {
            yield return StartCoroutine(battleEnd.GameOver());
            yield break;
        }

        //リザルト処理
        yield return StartCoroutine(battleResult.Do());
        //戦闘終了の処理
        yield return StartCoroutine(battleEnd.Win());
        isBattle = false;
        yield break;
    }

    public bool Settle()
    {
        return playerCharacters.All(x => x.IsDead()) || enemyCharacters.All(x => x.IsDead());
    }

    public bool IsLose()
    {
        return playerCharacters.All(x => x.IsDead());
    }

    public bool CanEscape()
    {
        return true;
    }

    public bool IsEscapeSuccess()
    {
        //30％で逃走が成功
        return Utility.CheckRate(30);
    }

    public void OnEscape()
    {
        escape = true;
    }

    public IEnumerator Escape()
    {
        MessageWindow messageWindow = BattleMessage.GetWindow();
        if (IsEscapeSuccess())
        {
            isBattle = false;
            yield return StartCoroutine(messageWindow.ShowAuto("逃げ切れた"));
            yield return StartCoroutine(SceneController.Instance.BackToField());
            yield break;
        }
        else
        {
            yield return StartCoroutine(messageWindow.ShowAuto("逃げ切れなかった"));
        }

        escape = false;
        yield break;
    }

    public int GetRewardExp()
    {
        return enemyCharacters.Sum(x => x.DropExp());
    }
}