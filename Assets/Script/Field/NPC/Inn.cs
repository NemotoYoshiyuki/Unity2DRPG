using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//宿屋
public class Inn : Interactable
{
    public int roomCharge = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("NPC");
    }

    public override void OnInteractable()
    {
        StartCoroutine(Talk());
    }

    public IEnumerator Talk()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();
        ChoiceWindow choiceWindow = MessageSystem.GetChoise();

        PlayerInteract.InteractableStart();

        yield return StartCoroutine(messageWindow.ShowClick("ひと晩" + roomCharge + "ゴールドですが\nおとまりになられますか"));
        yield return StartCoroutine(choiceWindow.Choice());
        int choiceResult = choiceWindow.Result();

        if (choiceResult == 1)
        {
            if (GameController.Instance.money <= 0)
            {
                yield return StartCoroutine(messageWindow.ShowClick("お金が　たりないようです"));
                messageWindow.Close();
                PlayerInteract.InteractableEnd();
                yield break;
            }

            yield return StartCoroutine(messageWindow.ShowClick("それでは　ごゆっくり おやすみください"));
            GameController.Instance.money -= roomCharge;
            Stay();
            yield return StartCoroutine(SceneFader.FadeSceneOut());
            yield return StartCoroutine(SceneFader.FadeSceneIn());
            yield return StartCoroutine(messageWindow.ShowClick("おはようございます。ではいってらっしゃいませ"));
        }
        else
        {
            yield return StartCoroutine(messageWindow.ShowClick("お気をつけて　旅をつづけられますように"));
        }

        messageWindow.Close();

        PlayerInteract.InteractableEnd();
        yield break;
    }

    public void Stay()
    {
        //var party = PlayerParty.Instance.partyMember;
        //foreach (var item in party)
        //{
        //    item.Recover(9999);
        //}
    }
}
