using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EncounterArea : MonoBehaviour
{
    public Sprite backGroundImage;
    public List<EnemyGroup> enemyGroups;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (EncounterController.Instance.canEncounter == false)
        {
            return;
        }

        if (!collision.GetComponent<PlayerMovement>().Moving())
        {
            return;
        }

        EncounterController.walkingSteps++;

        if (EncounterController.IsEncount)
        {
            //エンカウントする
            RandamEncounter();
        }
    }

    public void RandamEncounter()
    {
        Encounter encounter = Encounter.Instance;
        encounter.backGroundImage = backGroundImage;
        encounter.enemyGroups = GetEncountEnemy();

        EncounterController.SetEncounterSteps();
        SceneFader.FadeSceneOut();
        SceneController.Instance.Transition("Battle");
    }

    private List<EncountEnemy> GetEncountEnemy()
    {
        int randomChose = Lottery();

        if (randomChose == -1)
        {
            throw new System.Exception("EnemyGroupが正しく設定されていません");
        }
        return enemyGroups[randomChose].enemyGrop;
    }

    private int Lottery()
    {
        float total = enemyGroups.Select(x => x.rate).Sum();
        float randomPoint = Random.value * total;

        for (int i = 0; i < enemyGroups.Count; i++)
        {
            if (randomPoint < enemyGroups[i].rate)
            {
                return i;
            }

            randomPoint -= enemyGroups[i].rate;
        }
        return enemyGroups.Count - 1;
    }
}

[System.Serializable]
public class EnemyGroup
{
    [Range(1, 100)]
    public int rate;
    public List<EncountEnemy> enemyGrop;
}

[System.Serializable]
public class EncountEnemy
{
    public EnemyData enemy;
    [Range(0, 2)]
    public int posiiton;
}