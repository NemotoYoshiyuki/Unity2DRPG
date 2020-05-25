using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class CharacterSlot : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    public SelectableButton selectableButton;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI Lv;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Mp;

    public void Show()
    {
        Status status = playerCharacter.status;
        characterName.SetText(playerCharacter.CharacterName);
        Lv.SetText("Lv " + status.lv);
        Hp.SetText(status.hp + " / " + status.maxHp);
        Mp.SetText(status.mp + " / " + status.maxMp);
    }

    public void SetUp(PlayerCharacter playerCharacter)
    {
        this.playerCharacter = playerCharacter;
        Show();
    }

    private void Start()
    {
        selectableButton = GetComponent<SelectableButton>();
    }
}
