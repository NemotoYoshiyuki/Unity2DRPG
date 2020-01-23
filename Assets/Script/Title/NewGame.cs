using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    [SceneName]
    public string startScene;
    public PlayerData playerChacter;

    public void PlayNewGame()
    {
        SceneController.Instance.Transition(startScene);
    }
}
