using UnityEngine;
using TMPro;

public class CharacterSlot : MonoBehaviour
{
    public PlayerCharacter playerCharacter;

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
}
