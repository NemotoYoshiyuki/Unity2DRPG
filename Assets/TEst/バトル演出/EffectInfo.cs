using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo
{
    public Command command;//コマンド情報(Mp,TargetType.etc)
    public BattleCharacter owner;//自分
    public BattleCharacter target;//相手

    internal class EffectResult
    {
        //命中した等の情報
    }

    public EffectInfo(EffectInfo.CommandType commandType)
    {
        this.commandType = commandType;
    }

    //スキルor呪文
    public enum CommandType
    {
        スキル,
        呪文
    }

    public CommandType commandType;

    //魔法or物理
    public enum Characteristic
    {
        魔法,
        物理,
        特殊
    }
    public Characteristic characteristic;

    //効果分類
    public enum EffectType
    {
        攻撃,
        回復,
        補助
    }
    public EffectType effectType;


    //斬撃or打撃

    //属性
}
