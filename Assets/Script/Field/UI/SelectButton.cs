using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;


namespace UI.MenuUI
{
    public class SelectButton : Button
    {
        public Action onHover;
        public Action onHoverExit;

        public float duration = 2f;
        public float finalAlpha = 0.6f;

        private bool m_clickable;
        private TextMeshProUGUI textMeshProUGUI;
        private IEnumerator fade;

        public string text
        {
            set
            {
                if (textMeshProUGUI == null)
                {
                    textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
                }

                textMeshProUGUI.SetText(value);
            }
        }

        public bool clickable
        {
            get
            {
                return m_clickable;
            }
            set
            {
                m_clickable = value;
                ColorBlock colorBlock = this.colors;

                if (currentSelectionState == SelectionState.Selected)
                {
                    colorBlock.disabledColor = colors.selectedColor;
                }
                else
                {
                    colorBlock.disabledColor = colors.normalColor;
                }

                colors = colorBlock;
                interactable = value;
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            if (onHover != null) onHover.Invoke();

            if (clickable == false) return;
            if (transition != Transition.ColorTint) return;
            fade = OnHover(finalAlpha, duration);
            StartCoroutine(fade);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (onHoverExit != null) onHoverExit.Invoke();

            if (clickable == false) return;
            if (transition != Transition.ColorTint) return;
            StopCoroutine(fade);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (transition != Transition.ColorTint) return;
            StopCoroutine(fade);
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }

        private IEnumerator OnHover(float finalAlpha, float duration)
        {
            //waitを挿入
            yield return new WaitForEndOfFrame();

            float elapsedTime = 0;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);

            while (true)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0, finalAlpha, 1 / (duration / elapsedTime));

                alpha = Mathf.Repeat(alpha, finalAlpha);
                elapsedTime = Mathf.Repeat(elapsedTime, duration);
                image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

                yield return null;
            }
        }
    }
}
