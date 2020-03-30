using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInputController : MonoBehaviour
{
    public static BattleInputController instance;

    public BattleInputPhase inputPhase;
    public BattleCommandManager commandManager;

    private BattleCommandInput currentWindow;
    private PlayerCharacter CurrentInputCharacter;//現在入力中のキャラクター
    private Queue<PlayerCharacter> inputCharacters = new Queue<PlayerCharacter>();
    private BattleCommand battleCommand;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    { 
        inputCharacters.Clear();
        foreach (var player in BattleController.instance.AlivePlayerCharacters)
        {
            if (player.CanCommandInput())
            {
                inputCharacters.Enqueue(player);
            }
        }
        CurrentInputCharacter = inputCharacters.Dequeue();
    }

    public void SelectCommand(BattleCommand battleCommand)
    {
        this.battleCommand = battleCommand;
        TargetType targetType = battleCommand.GetTargetType();
        battleCommand.owner = CurrentInputCharacter;
        TargetFilter targetFilter = new TargetFilter();


        if (targetType.targetRange == TargetRange.単体)
        {
            var target = targetFilter.PlayerToGroup(battleCommand);
            OpenTargetWindow(target);
        }
        else
        {
            var target = targetFilter.PlayerToFilter(battleCommand);
            battleCommand.target = target;

            SetCommand(battleCommand);
        }
    }

    public void SelectTarget(BattleCharacter battleCharacter)
    {
        List<BattleCharacter> target = new List<BattleCharacter>() { battleCharacter };
        battleCommand.target = target;

        SetCommand(battleCommand);
    }

    private void SetCommand(BattleCommand battleCommand)
    {
        commandManager.Add(battleCommand);

        //コマンドの入力がすべて終わった
        if (IsCompleteInput())
        {
            currentWindow.Close();
            //入力処理の終了を通知します
            inputPhase.Finish();
        }
        else
        {
            CurrentInputCharacter = inputCharacters.Dequeue();
            Change<BattleFightInput>();
        }
    }

    private bool IsCompleteInput()
    {
        return inputCharacters.Count == 0;
    }

    public T Change<T>() where T : BattleCommandInput
    {
        if (currentWindow != null)
        {
            currentWindow.Close();
        }
        currentWindow = GetComponentInChildren<T>(true);
        currentWindow.Open();

        return currentWindow as T;
    }

    public void OnInputFinish()
    {
        currentWindow.Close();
        inputPhase.Finish();
    }

    public PlayerCharacter GetCurrentInputCharacter()
    {
        return CurrentInputCharacter;
    }

    public void OpenTargetWindow(List<BattleCharacter> target)
    {
        BattleTargetInput targetInput = Change<BattleTargetInput>();
        targetInput.Show(target);
    }

    private List<ItemData> itemDatas;
    public void OpenItemInputWindow()
    {
        itemDatas = GameController.GetInventorySystem().itemDatas;
        BattleItemInput itemInput = Change<BattleItemInput>();
        itemInput.Show(itemDatas);
        //アイテムリストをコピーして保存する
        //消費したアイテムを表示しないようにする
    }

    public void SkillInputWindow()
    {
        List<SkillData> skillDatas = CurrentInputCharacter.GetSkills();
        BattleSkillInput skillInputWindow = Change<BattleSkillInput>();
        skillInputWindow.Show(skillDatas);
    }

    public void SpellWindow()
    {
        List<SpellData> spellDatas = CurrentInputCharacter.GetSpells();
        BattleSpellInput spellInput = Change<BattleSpellInput>();
        spellInput.Show(spellDatas);
    }
}

