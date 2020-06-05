using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MenuUI
{
    public class _SideMenu : MonoBehaviour
    {
        public List<SelectButton> sideButtons;

        private void Start()
        {
            sideButtons[0].Select();
        }

        public void Lock()
        {
            foreach (var item in sideButtons)
            {
                item.clickable = false;
            }
        }

        public void Unlock()
        {
            foreach (var item in sideButtons)
            {
                item.clickable = true;
            }
        }
    }
}
