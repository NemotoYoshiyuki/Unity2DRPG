using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    [SceneName, Header("ゲーム開始時のシーン名")]
    public string startScene;
    [Header("ゲーム開始時の位置")]
    public Vector2 startPotisition;
    [Header("ゲーム開始時の所持アイテム")]
    public List<ItemData> startItem;
    [Header("主人公")]
    public PlayerData playerChacter;
    [Header("仲間")]
    public PlayerData friend1;
    public PlayerData friend2;
    public PlayerData friend3;

    public void PlayNewGame()
    {
        PlayerParty.Instance.Join(playerChacter);
        GameController.GetInventorySystem().itemDatas = new List<ItemData>(startItem);
        GameController.GetFlagManager().Init();
        GameController.Instance.resumeScene = startScene;
        GameController.Instance.checkpoint = startPotisition;
        SceneController.Instance.Transition(startScene, startPotisition);
    }
}
