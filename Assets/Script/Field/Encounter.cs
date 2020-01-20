using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    //フィールドシーンとバトルシーンを仲介する役割を持つオブジェクトです
    public Sprite backGroundImage;
    public AudioClip bgm;
    public List<EncountEnemy> enemyGroups;

    public static Encounter Instance;
    private void Awake()
    {
        Instance = FindObjectOfType<Encounter>();
        if (Instance == null)
        {
            GameObject gameObject = new GameObject("Encounter");
            Instance = gameObject.AddComponent<Encounter>();
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
}
