using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : Interactable
{
    [Header("ドアを開けるためのアイテム")]
    public Item keyItem;
    public GameObject door;

    private Collider2D triggerColider;
    private bool isOpen;
    private ulong id;
    private string sceneName => SceneController.Instance.CurrentScene;
    private string key => $"{sceneName}_ドアを開けた_{id}";

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        id = UnityEditor.GlobalObjectId.GetGlobalObjectIdSlow(gameObject).targetObjectId;
        triggerColider = GetComponent<Collider2D>();

        if (!VariablePersister.Exist(key)) return;
        isOpen = VariablePersister.GetBool(key);

        if (isOpen)
        {
            door.SetActive(false);
            triggerColider.enabled = false;
        }
    }

    public override void OnInteractable()
    {
        GameEvent.Execute(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        MessageWindow messageWindow = MessageSystem.GetWindow();
        bool hasItem = InventorySystem.HasItem(keyItem);
        if (!hasItem)
        {
            yield return StartCoroutine(messageWindow.ShowClick("ドアを開けるには鍵が必要なようだ"));
            messageWindow.Close();
        }
        else
        {
            yield return StartCoroutine(messageWindow.ShowClick("ガチャ！　ドアの鍵を開けた"));
            messageWindow.Close();

            //ドアを開ける
            door.SetActive(false);

            //イベントが起動しないようにする
            triggerColider.enabled = false;

            //鍵を消費する
            InventorySystem.Remove(keyItem);

            //フラグの保存
            VariablePersister.SetBool(key, true);
        }
        yield return null;
    }
}
