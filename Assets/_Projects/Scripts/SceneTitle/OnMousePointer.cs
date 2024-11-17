using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Title 
{
    public sealed class OnMousePointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Header("カーソルホバー時に、影響を受けるUI")] private Image hoverImageUI;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (hoverImageUI != null)
            {
                hoverImageUI.color = Color.red;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (hoverImageUI != null)
            {
                hoverImageUI.color = Color.white;
            }
        }

    }
}



