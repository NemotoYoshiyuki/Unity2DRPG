using UnityEngine;
using UnityEngine.Playables;

public abstract class Command : ScriptableObject
{
    public PlayableAsset animation;
    public string actionMessage = string.Empty;

    public abstract BattleCommand CreateBattleCommand();
}
