using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultPhase : MonoBehaviour
{
    public IEnumerator Do()
    {
        List<PlayerCharacter> aliveMember = PlayerParty.Instance.AliveMember();
        int dropExp = BattleController.instance.GetRewardExp();

        foreach (var alivePlayer in aliveMember)
        {
            alivePlayer.status.exp = dropExp;
            string ms = alivePlayer.CharacterName + "は" + dropExp + "けいけんちをかくとく";
            yield return StartCoroutine(MessageSystem.GetWindow().ShowClick(ms));
        }
        yield break;
    }
}
