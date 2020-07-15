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
    private string m_hp = string.Empty;
    private string m_mp = string.Empty;

    public void Initialized(PlayerCharacter playerCharacter)
    {
        this.playerCharacter = playerCharacter;
        characterName.SetText(playerCharacter.CharacterName);
    }

    private void Update()
    {
        SetText(playerCharacter.status.hp.ToString(), playerCharacter.status.mp.ToString());
    }

    private void SetText(string hp, string mp)
    {
        //文字列に変化がないとき更新しない
        if (hp == m_hp & mp == m_mp) return;
        hpText.SetText("Hp" + hp);
        mpText.SetText("Mp" + mp);
        m_hp = hp;
        m_mp = mp;
    }
}
