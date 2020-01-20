using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public bool canEncounter = true;
    public static int walkingSteps;//プレイヤーの歩数
    private static int encounterSteps;//エンカウントするまでの歩数
    public static bool isEncount = false;


    public static EncounterController Instance;
    private void Awake()
    {
        Instance = FindObjectOfType<EncounterController>();
        if (Instance == null)
        {
            GameObject gameObject = new GameObject("EncounterController");
            Instance = gameObject.AddComponent<EncounterController>();
        }

        if (Instance != this)
        {
            Destroy(gameObject);
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
        encounterSteps = Random.Range(50,300);
    }
}
