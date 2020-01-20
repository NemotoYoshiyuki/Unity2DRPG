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
        Encounter encounter = Encounter.Instance;
        SetBackGroundImage(encounter.backGroundImage);
        SetPlayAudio(encounter.bgm);
        GeneratePlayer();
        GenerateEnemy(encounter.enemyGroups);
        yield break;
    }

    public void GeneratePlayer()
    {
        List<PlayerCharacter> partyMember = PlayerParty.instance.partyMember;
        BattleController.instance.playerCharacters = partyMember;

        foreach (var player in partyMember)
        {
            statusWindow.Register(player);
        }
    }

    public void GenerateEnemy(List<EncountEnemy> enemies)
    {
        enemyParty.GenerateEnemy(enemies);
        BattleController.instance.enemyCharacters = enemyParty.EnemyCharacters;
    }

    public void SetBackGroundImage(Sprite image)
    {
        backGrounImage.sprite = image;
    }

    public void SetPlayAudio(AudioClip audioClip)
    {
        if (audioClip == null) return;

        backGroundMusic.clip = audioClip;
        backGroundMusic.Play();
    }

    //データのロード
    //所持品の読み込み
    //その他コンポーネントの初期化処理
}
