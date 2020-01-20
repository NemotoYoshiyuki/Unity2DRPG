using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusWindowElment : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI mp;

    public void Initialized(PlayerCharacter playerCharacter)
    {
        this.playerCharacter = playerCharacter;
    }

    private void Update()
    {
        characterName.SetText(playerCharacter.CharacterName);
        hp.SetText("Hp" + playerCharacter.status.hp.ToString());
        mp.SetText("Mp" + playerCharacter.status.mp.ToString());
    }
}
