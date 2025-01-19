using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Title
{
    public sealed class OnMousePointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Header("カーソルホバー時に、影響を受けるUI")] private Image _hoverImageUI;
        [SerializeField, Header("カーソルホバー時に、影響を受けるUI")] private TextMeshProUGUI _hoverTextUI;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_hoverImageUI == null) return;
            if (_hoverTextUI == null) return;

            _hoverImageUI.gameObject.SetActive(true);
            _hoverTextUI.color = Color.red;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverImageUI == null) return;
            if (_hoverTextUI == null) return;

            _hoverImageUI.gameObject.SetActive(false);
            _hoverTextUI.color = Color.white;
        }
    }
}