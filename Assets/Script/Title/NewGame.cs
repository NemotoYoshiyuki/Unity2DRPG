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

    public void PlayNewGame()
    {
        PlayerParty.Instance.Join(playerChacter);
        GameController.GetInventorySystem().itemDatas = new List<ItemData>(startItem);
        SceneController.Instance.Transition(startScene, startPotisition);
    }
}
