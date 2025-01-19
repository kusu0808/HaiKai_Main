using SO;
using UnityEngine;
using General;

namespace SceneGeneral
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            SScreenSetting.Entity.SerializedScreenSetting.Apply();
            SoundManager.BGMVolume = 0.0f;
            SoundManager.SEVolume = 0.0f;
        }
    }
}