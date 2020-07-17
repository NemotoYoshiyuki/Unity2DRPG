using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class CharacterSlot : MonoBehaviour
{
    //public PlayerCharacter playerCharacter;
    public CharacterData playerCharacter;
    public SelectableButton selectableButton;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI Lv;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Mp;

    public void Show()
    {
        Status status = playerCharacter.status;
        characterName.SetText(playerCharacter.GetName());
        Lv.SetText("Lv " + playerCharacter.lv);
        Hp.SetText("HP  " + status.hp + " / " + status.maxHp);
        Mp.SetText("MP  " + status.mp + " / " + status.maxMp);
    }

    //このメソッドを使え
    //public void SetUp(PlayerCharacter playerCharacter)
    //{
    //    this.playerCharacter = playerCharacter;
    //    Show();
    //}

    private void Start()
    {
        selectableButton = GetComponent<SelectableButton>();
    }
}
