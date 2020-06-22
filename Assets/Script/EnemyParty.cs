using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyParty : MonoBehaviour
{
    public GameObject enemyEntity;
    private List<EnemyCharacter> enemyCharacters = new List<EnemyCharacter>();
    public List<EnemyCharacter> EnemyCharacters => enemyCharacters;

    private List<Vector2> enemyPos = new List<Vector2>() {
            Vector2Int.zero,
            new Vector2Int(5, 0),
            new Vector2Int(-5, 0) };

    public void GenerateEnemy(List<EncountEnemy> enemies)
    {
        //敵を作成する
        foreach (var enemy in enemies)
        {
            enemyCharacters.Add(CreateEnemy(enemy));
        }

        //敵に名前をつける
        Naming();
    }

    public EnemyCharacter CreateEnemy(EncountEnemy encountEnemy)
    {
        //EnemyDataの値を書き換えないようにするためコピーしたものを使います
        EnemyData enemyData = ScriptableObject.Instantiate(encountEnemy.enemy);
        int pos = encountEnemy.posiiton;

        GameObject enemy = Instantiate(enemyEntity);
        enemy.transform.position = enemyPos[pos];

        //グラフィックの設定
        SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();
        sprite.sprite = enemyData.Graphic;

        //ステータスの設定
        EnemyCharacter enemyCharacter = enemy.GetComponent<EnemyCharacter>();
        enemyCharacter.enemyData = enemyData;
        enemyCharacter.Initialize(enemyData.Status);

        //名前の設定
        enemyCharacter.CharacterName = enemyCharacter.enemyData.CharacterName;
        enemyCharacter.gameObject.name = enemyCharacter.CharacterName;
        enemyCharacter.gameObject.transform.SetParent(gameObject.transform);
        return enemyCharacter;
    }

    private void Naming()
    {
        //同じ名前の敵が２体以上存在する時、識別のためにアルファベットを付け足します

        //同じな名前の敵でグルーピングします
        var enemyGroup = enemyCharacters.GroupBy(x => new { Name = x.CharacterName });

        foreach (var group in enemyGroup)
        {
            if (group.Count() <= 1) continue;

            //同じ名前の敵が２体以上格納されているListです
            var groupEnemy = group.ToList();
            for (int i = 0; i < groupEnemy.Count; i++)
            {
                string newName = groupEnemy[i].CharacterName + GetAlphabet(i);
                groupEnemy[i].CharacterName = newName;
                groupEnemy[i].gameObject.name = newName;
            }
        }
    }

    private char GetAlphabet(int number)
    {
        //ASCIIコード65はAです
        int ASCII_Code = 65 + number;
        return (char)ASCII_Code;
    }

    public void CallEnemy()
    {
        //仲間を呼ぶ
    }

    public void EsCape()
    {

    }
}
