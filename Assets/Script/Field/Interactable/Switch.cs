using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : Interactable
{
    public Sprite onImage;
    public Sprite offImage;

    public UnityEvent OnSwitch;
    public UnityEvent OffSwitch;

    public float wait = 1f;
    public string onMessage = "スイッチをオンにした";
    public string offMessage = "スイッチをオフにした";

    private SpriteRenderer spriteRenderer;
    private bool isOnSwitch = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public override void OnInteractable()
    {
        StartCoroutine(Switching());
    }

    public IEnumerator Switching()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();

        InteractableStart();

        //スイッチがオンのときオフに切り替える
        //Switch off when switch is on
        if (isOnSwitch)
        {
            isOnSwitch = false;
            spriteRenderer.sprite = offImage;
            OffSwitch.Invoke();

            yield return new WaitForSeconds(wait);
            yield return StartCoroutine(messageWindow.ShowClick(offMessage));
        }
        else
        {
            isOnSwitch = true;
            spriteRenderer.sprite = onImage;
            OnSwitch.Invoke();

            yield return new WaitForSeconds(wait);
            yield return StartCoroutine(messageWindow.ShowClick(onMessage));
        }

        messageWindow.Close();
        InteractableEnd();
        yield break;
    }

}
