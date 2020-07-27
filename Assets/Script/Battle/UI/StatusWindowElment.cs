using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusWindowElment : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI mpText;
    public TextMeshProUGUI condition;

    public void Initialized(PlayerCharacter playerCharacter)
    {
        this.playerCharacter = playerCharacter;
        characterName.SetText(playerCharacter.CharacterName);
    }

    private void Update()
    {
        SetText(playerCharacter.status.hp, playerCharacter.status.mp);
    }

    private void SetText(int hp, int mp)
    {
        //文字列に変化がないとき更新しない
        hpText.text = "Hp" + hp;
        mpText.text = "Mp" + mp;

        if (hp <= 0) condition.text = "しぼう";
        else if (playerCharacter.GetStatusEffect() != null) condition.text = playerCharacter.GetStatusEffect().alimentName;
        else condition.text = string.Empty;
    }
}