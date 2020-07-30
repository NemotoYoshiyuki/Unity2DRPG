using UnityEngine;
public class Equipment : ScriptableObject
{
    public uint id;
    public new string name;

    [Header("装備時のステータス上昇値")]
    public int maxHp;
    public int maxMp;
    public int attack;
    public int deffence;
    public int speed;

    [Header("説明文")]
    public string description;
}
