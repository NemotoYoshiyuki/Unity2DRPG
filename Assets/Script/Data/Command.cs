using UnityEngine;
using UnityEngine.Playables;

public abstract class Command : ScriptableObject
{
    public PlayableAsset animation;

    public abstract BattleCommand CreateBattleCommand();
}
