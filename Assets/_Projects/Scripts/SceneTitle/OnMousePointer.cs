using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Title 
{
    public sealed class OnMousePointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Header("カーソルホバー時に、影響を受けるUI")] private Image _hoverImageUI;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_hoverImageUI != null)
            {
                _hoverImageUI.color = Color.red;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverImageUI != null)
            {
                _hoverImageUI.color = Color.white;
            }
        }

    }
}