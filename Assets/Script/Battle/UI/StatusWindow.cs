using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatusWindow : MonoBehaviour
{
    public StatusWindowElment statusWindowElment;
    public Transform horizontalLayout;

    public void Register(PlayerCharacter playerCharacter)
    {
        StatusWindowElment _statusWindowElment = Instantiate(statusWindowElment);
        _statusWindowElment.Initialized(playerCharacter);
        _statusWindowElment.transform.SetParent(horizontalLayout);
    }
}