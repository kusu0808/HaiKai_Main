using General;
using UnityEngine;

public static class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        SoundManager.BGMVolume = 0.0f;
        SoundManager.SEVolume = 0.0f;
    }
}