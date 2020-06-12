
[System.Serializable]
public class CharacterData
{
    public int id;
    public int lv;
    public string name;//自分で決められる場合
    public PlayerData playerData;//初期データ
    public Status status;//現在のステータス
    public Equip equip;//装備情報
    //種補正
    public StatusEffectType statusEffectType;//状態異常

    public CharacterData(PlayerData playerData)
    {
        this.playerData = playerData;
        //初期の設定
        this.status = playerData.Status.Copy();
        //初期装備

    }

    public Status GetBaseStatus()
    {
        //補正を含まないステータスを返す
        return status;
    }

    public Status GetStatus()
    {
        //装備の補正を含んだステータスを返す
        return status;
    }

    public CharacterData Copy()
    {
        return (CharacterData)this.MemberwiseClone();
    }

    //体力の増減　床ダメージ
}