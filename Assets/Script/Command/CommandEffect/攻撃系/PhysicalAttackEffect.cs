using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttackEffect : Effect
{
    public float damageRate = 1;
    public bool critical;
    public int criticalRate = 32;//会心率
    public int hitRate = 100;//命中率
    public int successRate = 100;//成功率
    public string successMessage = string.Empty;//成功時メッセージ
    public string failureMessage;//失敗時メッセージ

    private BattleDirectorController logic;

    public override void Use(BattleCharacter owner, BattleCharacter target)
    {
        logic = BattleDirectorController.Instance;

        //成功判定
        bool m_success = BattleFormula._CheckRate(successRate);
        if (!m_success)
        {
            //失敗メッセージを表示
            logic.Message(target.CharacterName + failureMessage);
            return;
        }

        //命中
        bool m_hit = BattleFormula._CheckRate(hitRate);
        if (!m_hit)
        {
            //Miss
            logic.Miss(target);
            return;
        }

        int damage = BattleFormula.PhysicalAttackDamage(owner.status, target.status);
        damage = Mathf.RoundToInt(damage * damageRate);

        //クリティカル
        bool m_critical = BattleFormula._CheckRate(1);
        if (m_critical)
        {
            damage = Mathf.RoundToInt(damage * 1.5f);
        }

        //成功メッセージを表示
        if (successMessage != string.Empty) logic.Message(owner.CharacterName + successMessage);
        logic.DamageLogic(target, damage, m_critical);
    }
}