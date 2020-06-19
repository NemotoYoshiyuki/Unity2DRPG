using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public class BattleDirectorController : MonoBehaviour
{
    private List<BattleDirector> battleDirectors = new List<BattleDirector>();

    //SE
    public AudioClip critical;
    public AudioClip miss;
    public AudioClip hit;
    public AudioClip avoidance;
    public AudioClip escape;

    //PlayableAsset
    public PlayableAsset slash;
    public PlayableAsset blow;

    public static BattleDirectorController Instance;
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Play()
    {
        foreach (var item in battleDirectors)
        {
            yield return item.Execute();
        }

        battleDirectors.Clear();
        yield break;
    }

    public void Add(BattleDirector director)
    {
        battleDirectors.Add(director);
    }

    public void NormalAttack(BattleCharacter character)
    {
        //武器によってアニメーションを変更
        AnimationPlay(slash, character.transform);
    }

    public void Miss(BattleCharacter character)
    {
        Add(new SoundDirector(miss));
        Add(new MessageDirector($"ミス！\n　{character.CharacterName}は　ダメージを　うけない！"));
    }

    public void Hit(BattleCharacter character)
    {
        Add(new SoundDirector(hit));
        Add(new HitDirector(character));
    }

    public void Avoidance(BattleCharacter character)
    {
        Add(new SoundDirector(hit));
        Add(new MessageDirector($"{character.CharacterName}は　ひらりと　みをかわした！"));
    }

    public void Escape(BattleCharacter character)
    {
        Add(new SoundDirector(escape));
        Add(new MessageDirector($"{character.CharacterName}は　にげだした！"));
    }

    //物理ダメージ処理
    public void DamageLogic(EffectInfo info, int damage, bool isCritical = false)
    {
        //回避判定
        if (IsAvoidance(info.target))
        {
            Avoidance(info.target);
            return;
        }

        if (damage <= 0)
        {
            //ダメージ0以下
            Add(new MessageDirector(info.target + "に　ダメージを　あたえられない！"));
            return;
        }

        if (isCritical)
        {
            Critical(info.target, damage);
        }
        else
        {
            //ダメージ演出を生成します
            Hit(info.target);
            Add(new DamageDirector(info.target, damage));
            Add(new MessageDirector(info.target.CharacterName + "は" + damage + "うけた"));
        }

        ////対象のHPが0になった
        if (info.target.status.hp < damage)
        {
            Dead(info.target);
            return;
        }

        BattleController.instance.onDamage?.Invoke(info);
    }

    public void Damage(BattleCharacter target, int damage, string message)
    {
        Hit(target);
        Add(new DamageDirector(target, damage, message));

        if (target.status.hp < damage)
        {
            Dead(target);
        }
    }

    public void Critical(BattleCharacter battleCharacter, int damage)
    {
        Add(new SoundDirector(critical));
        Add(new DamageDirector(battleCharacter, damage));
        Add(new MessageDirector($"かいしんのいちげき！\n{battleCharacter.CharacterName}に　{damage}のダメージ！！"));
    }

    public void Heal(BattleCharacter character, int healAmount)
    {
        Add(new HealDirectpr(character, healAmount));
        Add(new MessageDirector($"{character.CharacterName}の　キズが　かいふくした！"));
    }

    public void Revival(BattleCharacter character, int healAmount)
    {
        Add(new HealDirectpr(character, healAmount));
        Add(new MessageDirector($"{character.CharacterName}は　いきかえった！"));
    }

    public void AddStatusEffect(BattleCharacter battleCharacter, StatusEffect statusEffect)
    {
        //状態異常を付与します
        Add(new AddStatusEffectDirector(battleCharacter, statusEffect));
    }

    public void RemoveStatusEffect(BattleCharacter battleCharacter)
    {
        StatusEffect statusEffect = battleCharacter.GetStatusEffect();

        Add(new RemoveStatusEffectDirector(battleCharacter, statusEffect));
    }

    public void AddBuff(BattleCharacter battleCharacter, Buff buff)
    {
        Add(new AddBuffDirector(battleCharacter, buff));
    }

    public bool IsAvoidance(BattleCharacter character)
    {
        //敵の回避率を参照
        return BattleFormula._CheckRate(1);
    }

    private void Dead(BattleCharacter character)
    {
        Add(new DeadDirector(character));
    }

    public void Message(string message)
    {
        Add(new MessageDirector(message));
    }

    public void AnimationPlay(PlayableAsset playableAsset, Transform transform = null)
    {
        Add(new AnimationDirector(playableAsset, transform));
    }
}
