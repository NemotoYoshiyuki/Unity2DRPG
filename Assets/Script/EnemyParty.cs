using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyParty : MonoBehaviour
{
    public GameObject enemyEntity;
    private List<EnemyCharacter> enemyCharacters = new List<EnemyCharacter>();
    public List<EnemyCharacter> EnemyCharacters => enemyCharacters;

    public void GenerateEnemy(List<EncountEnemy> enemies)
    {

        List<Vector2> enemyPos = new List<Vector2>() {
            Vector2Int.zero,
            new Vector2Int(5, 0),
            new Vector2Int(-5, 0) };

        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = Instantiate(enemyEntity);
            enemy.transform.position = enemyPos[i];

            //グラフィックの設定
            SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();
            sprite.sprite = enemies[i].enemy.graphic;

            //ステータスの設定
            EnemyCharacter enemyCharacter = enemy.GetComponent<EnemyCharacter>();

            //EnemyDataの値を書き換えないようにするためコピーしたものを使います
            enemyCharacter.enemyData = ScriptableObject.Instantiate(enemies[i].enemy);
            enemyCharacter.basicStatus = enemyCharacter.enemyData.status;
            enemyCharacter.SetUp();

            //名前の設定
            enemyCharacter.gameObject.name = enemyCharacter.CharacterName;
            enemyCharacter.gameObject.transform.SetParent(gameObject.transform);

            enemyCharacters.Add(enemyCharacter);
        }

        //同じ名前の敵が２体以上存在する時、識別のためにアルファベットを付け足します

        //同じな名前の敵でグルーピングします
        var enemyGroup = enemyCharacters.GroupBy(x => new { Name = x.CharacterName });
        foreach (var group in enemyGroup)
        {
            if (group.Count() >= 2)
            {
                //同じ名前の敵が２体以上格納されているListです
                var groupEnemy = group.ToList();
                //ASCIIコード65はAです
                int ASCII_Code = 65;
                for (int i = 0; i < groupEnemy.Count; i++)
                {
                    string newName = groupEnemy[i].CharacterName + (char)ASCII_Code++;
                    groupEnemy[i].CharacterName = newName;
                    groupEnemy[i].gameObject.name = newName;
                }
            }
        }
    }

    public void CallEnemy()
    {
        //仲間を呼ぶ
    }

    public void EsCape()
    {

    }
}
