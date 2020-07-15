using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatusWindow : MonoBehaviour
{
    public StatusWindowElment[] statusWindowElment;
    private int count = 0;

    public void Register(PlayerCharacter playerCharacter)
    {
        statusWindowElment[count].gameObject.SetActive(true);
        statusWindowElment[count].Initialized(playerCharacter);
        count++;
    }
}