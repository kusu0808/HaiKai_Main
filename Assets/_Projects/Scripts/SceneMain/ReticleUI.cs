using UnityEngine;
using UnityEngine.UI;

namespace Main.Eventer.UIElements
{
    /// <summary>
    /// レティクルUI　レイキャストが当たった際色を変更
    /// </summary>
    public sealed class ReticleUI : MonoBehaviour
    {
        [SerializeField] private Image _reticleImage;

        public void ChangeColor(bool onRaycastHit)
        {
            if (_reticleImage == null) return;
            _reticleImage.color = onRaycastHit ? Color.red : Color.white;
        }
    }
}