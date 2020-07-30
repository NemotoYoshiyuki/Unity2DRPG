using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class CharacterData
{
    public int id;
    public int lv;
    public int exp;//経験値
    public string name;//自分で決められる場合
    public PlayerData playerData;//初期データ
    public Status status;//現在のステータス
    public Equip equip = new Equip();//装備情報
    //種補正
    public StatusEffectType statusEffectType;//状態異常

    public CharacterData(PlayerData playerData)
    {
        this.id = playerData.CharacterID;
        this.playerData = playerData;
        //初期の設定
        this.status = playerData.Status.Copy();
        this.lv = playerData.Status.lv;
        //初期装備
        equip = new Equip();
        equip.weapon = playerData.Equip.weapon;
        equip.armor = playerData.Equip.armor;
        equip.accessory = playerData.Equip.accessory;
    }

    public string GetName()
    {
        return playerData.CharacterName;
    }

    public Status GetBaseStatus()
    {
        //補正を含まないステータスを返す
        return status;
    }

    public Status GetStatus()
    {
        //装備の補正を含んだステータスを返す
        Status m_status = status.Copy();
        m_status.maxHp += equip.GetMaxHp();
        m_status.maxMp += equip.GetMaxMp();
        m_status.attack += equip.GetAttack();
        m_status.deffence += equip.GetDeffence();
        m_status.speed += equip.GetSpeed();
        return m_status;
    }

    public void FullRecover()
    {
        Recover(9999);
        RecoverMp(9999);
    }

    public void Recover(int amount)
    {
        status.hp = Mathf.Clamp(status.hp + amount, 0, status.maxHp);
    }

    public void RecoverMp(int amount)
    {
        status.mp = Mathf.Clamp(status.mp + amount, 0, status.maxMp);
    }

    public List<Spell> GetSpells()
    {
        return playerData.SpellDatas.Where(x => x.lv < lv).Select(x => x.spell).ToList();
    }

    public List<Skill> GetSkills()
    {
        return playerData.SkillDatas.Where(x => x.lv < lv).Select(x => x.skill).ToList();
    }

    public int NextExp(int lv)
    {
        if (lv <= 99) return 0;
        List<int> experienceTable = ExperienceTable.Get();
        return experienceTable[lv + 1];
    }

    public CharacterData Copy()
    {
        return (CharacterData)this.MemberwiseClone();
    }
}