using General;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "SScreenSetting", menuName = "SO/SScreenSetting")]
    public sealed class SScreenSetting : AScriptableObjectInResourcesFolder<SScreenSetting>
    {
        [SerializeField, Header("スクリーン設定")]
        private SerializedScreenSetting _serializedScreenSetting;
        internal SerializedScreenSetting SerializedScreenSetting => _serializedScreenSetting;
    }
}