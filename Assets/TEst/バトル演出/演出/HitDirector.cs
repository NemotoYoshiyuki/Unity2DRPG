using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDirector : BattleDirector
{
    public BattleCharacter character;

    public HitDirector(BattleCharacter character)
    {
        this.character = character;
    }

    public override IEnumerator Do()
    {
        //キャラクターを点滅させる
        SpriteRenderer spriteRenderer = character.gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer.sprite == null) yield break;

        Color color = spriteRenderer.color;
        int count = 0;
        int a = 1;

        //奇数回繰り返す
        while (7 >= count)
        {
            //符号を反転
            a = -a;
            //負の数は0として扱われる
            color.a = a;
            spriteRenderer.color = color;

            count++;
            yield return new WaitForSeconds(0.1f);
        }
        yield break;
    }
}
