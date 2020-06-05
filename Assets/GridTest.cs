using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTest : MonoBehaviour
{
    public List<SelectableButton> _SelectButtons;
    public List<Selectable> selectables;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return Wait();
        foreach (var item in _SelectButtons)
        {
            item.FindSelectable(selectables);
            //Debug.Log(item.gameObject.GetComponent<RectTransform>().position);
        }

        selectables[0].Select();
        yield break;
    }

    public IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
    }
}
