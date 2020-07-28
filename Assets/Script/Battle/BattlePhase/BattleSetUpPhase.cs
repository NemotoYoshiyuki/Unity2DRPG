using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//バトルシーンが開始した時一番最初に呼ばれなければいけません
public class BattleSetUpPhase : MonoBehaviour
{
    public PlayerParty playerParty;
    public EnemyParty enemyParty;

    public StatusWindow statusWindow;
    public SpriteRenderer backGrounImage;
    public AudioSource backGroundMusic;

    public IEnumerator SetUp()
    {
        EncounterController encounter = EncounterController.Instance;
        SetBackGroundImage(encounter.backGroundImage);
        SetPlayAudio(encounter.bgm);
        SetBattleSetting(encounter.canEscape);
        GeneratePlayer();
        GenerateEnemy(encounter.enemyGroups);
        yield break;
    }

    private void GeneratePlayer()
    {
        List<CharacterData> party = Party.GetMember();
        playerParty.GeneratePlayer(party);
        List<PlayerCharacter> partyMember = playerParty.partyMember;
        BattleController.instance.playerCharacters = partyMember;

        foreach (var player in partyMember)
        {
            statusWindow.Register(player);
        }
    }

    private void GenerateEnemy(List<EncountEnemy> enemies)
    {
        enemyParty.GenerateEnemy(enemies);
        BattleController.instance.enemyCharacters = enemyParty.EnemyCharacters;
    }

    private void SetBattleSetting(bool canEscape)
    {
        BattleController.instance.canEscape = canEscape;
    }

    private void SetBackGroundImage(Sprite image)
    {
        if (image == null) return;
        backGrounImage.sprite = image;
    }

    private void SetPlayAudio(AudioClip audioClip)
    {
        if (audioClip == null) return;

        backGroundMusic.clip = audioClip;
        backGroundMusic.Play();
    }
}
