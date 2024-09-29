using General;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "ScreenSetting", menuName = "SO/ScreenSetting")]
    public class SScreenSetting : ScriptableObject
    {
        [SerializeField, Header("ƒXƒNƒŠ[ƒ“Ý’è")]
        private SerializedScreenSetting _serializedScreenSetting;
        internal SerializedScreenSetting SerializedScreenSetting => _serializedScreenSetting;
    }
}