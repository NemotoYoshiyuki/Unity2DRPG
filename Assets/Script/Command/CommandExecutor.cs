using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{
    public MessageWindow message;
    public CommandEffectExecutor effectExecutor;
    public CommandDirectionExecutor directionExecutor;

    private void Start()
    {
        message = BattleMessage.GetWindow();
    }

    //バトルコマンドを実行します
    //継承している型でコマンド動作を決定します
    public IEnumerator _Execution(BattleCommand battleCommand)
    {
        switch (battleCommand)
        {
            //バトルコマンドがAttackCommandを継承しているなら
            case AttackCommand attackCommand:
                yield return StartCoroutine(Attack(attackCommand));
                break;
            case DefenseCommand defenseCommand:
                yield return StartCoroutine(Defence(defenseCommand));
                break;
            case SkillCommand skillCommand:
                yield return StartCoroutine(Skill(skillCommand));
                break;
            case SpellCommand spellCommand:
                yield return StartCoroutine(Spell(spellCommand));
                break;
            case ItemCommand itemCommand:
                yield return Item(itemCommand);
                break;
            default:
                break;
        }
        yield break;
    }

    public IEnumerator Execution(BattleCommand battleCommand)
    {
        //コマンド実行
        yield return StartCoroutine(battleCommand.Execution());

        //コマンドの効果実行
        yield return effectExecutor.Execution(battleCommand);
        yield break;
    }

    public IEnumerator Attack(AttackCommand attackCommand)
    {
        string attackMessage = attackCommand.owner.CharacterName + "のこうげき";
        yield return StartCoroutine(message.ShowAuto(attackMessage));

        //directionExecutor.Hit();

        //BattleCommandの効果処理を行います
        yield return StartCoroutine(effectExecutor.Execution(attackCommand));
        yield break;
    }

    public IEnumerator Defence(DefenseCommand defenseCommand)
    {
        string defenceMessage = defenseCommand.owner.CharacterName + "はみをまもった";
        yield return StartCoroutine(message.ShowAuto(defenceMessage));

        yield return StartCoroutine(effectExecutor.Execution(defenseCommand));
        yield break;
    }

    public IEnumerator Skill(SkillCommand skillCommand)
    {
        BattleCharacter owner = skillCommand.owner;
        //string skillName = skillCommand.skillData.skillName;
        int skillMp = skillCommand.skillData.mp;
        //string skillMessage = skillCommand.owner.CharacterName + "は" + skillName + "をつかった";
        //yield return StartCoroutine(message.ShowAuto(skillMessage));

        if (owner.status.mp <= skillMp)
        {
            yield return StartCoroutine(message.ShowAuto("しかし ＭＰが たりない！"));
            yield break;
        }

        owner.GainMp(skillMp);
        yield return StartCoroutine(effectExecutor.Execution(skillCommand));
        yield break;
    }

    public IEnumerator Spell(SpellCommand spellCommand)
    {
        BattleCharacter owner = spellCommand.owner;
        int spellMp = spellCommand.spellData.mp;
        //string spellMessage = spellCommand.owner.CharacterName + "をとなえた";
        string spellMessage = $"{spellCommand.owner.CharacterName}は　{spellCommand.spellData.skillName}をとなえた";
        yield return StartCoroutine(message.ShowAuto(spellMessage));

        if (owner.status.mp <= spellMp)
        {
            yield return StartCoroutine(message.ShowAuto("しかし ＭＰが たりない！"));
            yield break;
        }

        owner.GainMp(spellMp);
        yield return StartCoroutine(effectExecutor.Execution(spellCommand));
        yield break;
    }

    public IEnumerator Item(ItemCommand itemCommand)
    {
        string itemName = itemCommand.item.itemName;
        string itemMessage = itemCommand.owner.CharacterName + "は" + itemName + "をつかった";
        yield return StartCoroutine(message.ShowAuto(itemMessage));

        GameController.GetInventorySystem().UseItem(itemCommand.item);

        yield return StartCoroutine(effectExecutor.Execution(itemCommand));
        yield break;
    }
}
