using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestSelect : MonoBehaviour,IPointerClickHandler
{
    public Button m_button;

    public void OnPointerClick(PointerEventData eventData)
    {
        ((IPointerClickHandler)m_button).OnPointerClick(eventData);
        Debug.Log("2");
    }

    // Start is called before the first frame update
    void Start()
    {

        //Button button = Instantiate(m_button, Vector3Int.up * 30, transform.rotation);
        //Button button1 = Instantiate(m_button, Vector3Int.down, transform.rotation);
        //button.transform.SetParent(gameObject.transform, false);
        //button1.transform.SetParent(gameObject.transform, false);
        //// 自分を選択状態にする
        ////Selectable sel = GetComponent<Selectable>();
        ////sel.Select();
        //button.Select();

        m_button.Select();
        var b = m_button.FindSelectableOnRight();
        Debug.Log(b);
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
