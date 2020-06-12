using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Playables;

public class _BattleLogic : MonoBehaviour
{
    private Queue<BattleDirector> battleDirectors = new Queue<BattleDirector>();

    //SE
    public AudioClip critical;
    public AudioClip miss;
    public AudioClip hit;
    public AudioClip avoidance;
    public AudioClip escape;

    //トリガー
    public Action onActionBefore = null;
    public Action<EffectInfo> onDamage = null;
    public Action onTurnEnd = null;

    public static _BattleLogic Instance;
    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator Play()
    {
        foreach (var item in battleDirectors)
        {
            yield return item.Do();
        }

        battleDirectors.Clear();
        yield break;
    }

    public void Add(BattleDirector director)
    {
        battleDirectors.Enqueue(director);
    }

    public void StartAction(AnimationClip animationClip, string message)
    {
        Add(new MessageDirector(message));
        Add(new AnimationDirector(animationClip));
    }

    public void NormalAttack()
    {
        //武器によってアニメーションを変更
    }

    //物理ダメージ処理
    public void DamageLogic(EffectInfo info, int damage, bool critical = false)
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
        else
        {
            if (critical)
            {
                Critical(damage, info.target);
            }
            else
            {
                //ダメージ演出を生成します
                Hit(info.target);
                Add(new DamageDirector(damage, info.target));
                Add(new MessageDirector(info.target.CharacterName + "は" + damage + "うけた"));
            }

            ////対象のHPが0になった
            if (info.target.status.hp < damage)
            {
                Dead(info.target);
                return;
            }

            //被ダメージ
            //ダメージトリガー
            //StatusEffect statusEffect = info.target.statusEffect;
            //if (statusEffect == null) return;
            //statusEffect.OnDamage();

            onDamage?.Invoke(info);
        }
    }

    public void Damage(BattleCharacter target, int damage, string message)
    {
        Add(new DamageDirector(damage, target));
        Message(message);

        //ステータスのコピーを参照する？

        ////対象のHPが0になった
        if (target.status.hp < damage)
        {
            Dead(target);
        }
    }

    public void Critical(int damage, BattleCharacter battleCharacter)
    {
        Add(new SoundDirector(critical));
        Add(new DamageDirector(damage, battleCharacter));
        Add(new MessageDirector($"かいしんのいちげき！\n{battleCharacter.CharacterName}に　{damage}のダメージ！！"));

    }

    public void Heal(int healAmount, BattleCharacter character)
    {
        Add(new HealDirectpr(character, healAmount));
        Add(new MessageDirector($"{character.CharacterName}の　キズが　かいふくした！"));
    }

    public void Revival(int healAmount, BattleCharacter character)
    {
        Add(new HealDirectpr(character, healAmount));
        Add(new MessageDirector($"{character.CharacterName}は　いきかえった！"));
    }

    public void AddStatusEffect(BattleCharacter battleCharacter, StatusEffect statusEffect)
    {
        //状態異常を付与します
        Add(new AddStatusEffectDirector(battleCharacter, statusEffect));
        Message(statusEffect.onAdd);
    }

    public void RemoveStatusEffect(BattleCharacter battleCharacter)
    {
        StatusEffect statusEffect = battleCharacter.statusEffect;
        //状態異常を解除します
        Add(new RemoveStatusEffectDirector(battleCharacter, statusEffect));
        Message(statusEffect.resolution);
    }

    public void AddBuff(BattleCharacter battleCharacter, Buff buff)
    {
        Add(new SupportEffectDirector(battleCharacter, buff));
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

    public bool IsAvoidance(BattleCharacter character)
    {
        //敵の回避率を参照
        return BattleFormula._CheckRate(1);
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

    //物理ダメージ
    private void Damage(int damage, BattleCharacter battleCharacter)
    {
        Add(new DamageDirector(damage, battleCharacter));
    }

    private void Dead(BattleCharacter character)
    {
        Add(new DeadDirector(character));
    }

    //計算

    //その他
    public void Message(string message)
    {
        Add(new MessageDirector(message));
    }

    public void AnimationPlay(PlayableAsset playableAsset)
    {
        Add(new AnimationDirector(playableAsset));
    }

    public void AnimationPlay(PlayableAsset playableAsset, Transform transform)
    {
        Add(new AnimationDirector(playableAsset, transform));
    }
}
