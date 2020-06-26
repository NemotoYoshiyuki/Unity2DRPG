using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EncounterController : MonoBehaviour
{
    public EnemyMasterData enemyMasterData;
    public bool canEncounter = true;
    public static int walkingSteps;//プレイヤーの歩数
    private static int encounterSteps;//エンカウントするまでの歩数
    public static bool isEncount = false;

    [Tooltip("バトルデータ")]
    public Sprite backGroundImage;
    public AudioClip bgm;
    public List<EncountEnemy> enemyGroups;


    public static EncounterController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = FindObjectOfType<EncounterController>();
        if (Instance == null)
        {
            GameObject gameObject = new GameObject("EncounterController");
            Instance = gameObject.AddComponent<EncounterController>();
        }
        DontDestroyOnLoad(this);
    }

    public static bool IsEncount => walkingSteps > encounterSteps;

    private void Start()
    {
        SetEncounterSteps();
    }

    public static void SetEncounterSteps()
    {
        walkingSteps = 0;
        encounterSteps = Random.Range(50, 300);
    }

    public static void Encount(List<EncountEnemy> enemies, Sprite backGroundImage = null, AudioClip bgm = null)
    {
        Instance.backGroundImage = backGroundImage;
        Instance.enemyGroups = enemies;

        SetEncounterSteps();
        Instance.StartCoroutine(SceneFader.FadeSceneOut());
        SceneController.Instance.Transition("Battle 1");
    }

    public static void Encount(int id)
    {
        EnemyData enemyData = Instance.enemyMasterData.characterData.First(x => x.Id == id);
        var enemyGroups = new List<EncountEnemy>() { new EncountEnemy() { enemy = enemyData, posiiton = 0 } };
        Encount(enemyGroups);
    }
}