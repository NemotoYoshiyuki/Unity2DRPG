using UnityEngine;
public class Equipment : ScriptableObject
{
    public new string name;

    [Header("装備時のステータス上昇値")]
    public int maxHp;
    public int maxMp;
    public int attack;
    public int deffence;
    public int speed;
    //特殊効果
}
