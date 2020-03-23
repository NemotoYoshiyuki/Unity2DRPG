using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSystem : MonoBehaviour
{
    public GameObject MenuWindow;
    public bool canOpen = true;
    public static WindowSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canOpen) return;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (MenuWindow.activeSelf) MenuWindow.SetActive(false);
            else MenuWindow.SetActive(true);
        }
    }
}
