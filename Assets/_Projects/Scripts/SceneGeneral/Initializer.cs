using Cysharp.Threading.Tasks;
using SO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using AsyncOperationStatus = UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus;
using General;

namespace SceneGeneral
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static async void Initialize()
        {
            await InitializeScreenSetting();
            await InitializeAudioMixer();

            SoundManager.BGMVolume = 0.0f;
            SoundManager.SEVolume = 0.0f;
        }

        private static async UniTask InitializeScreenSetting()
        {
            var handle = Addressables.LoadAssetAsync<SScreenSetting>("ScreenSetting");
            await handle.Task.AsUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded) handle.Result.SerializedScreenSetting.Apply();
            Addressables.Release(handle);
            "ScreenSettingの初期化完了".Tell("00ffff");
        }

        private static async UniTask InitializeAudioMixer()
        {
            var handle = Addressables.LoadAssetAsync<AudioMixer>("AM");
            await handle.Task.AsUniTask();
            if (handle.Status == AsyncOperationStatus.Succeeded) SoundManager.AM = handle.Result;
            "AMの初期化完了(解放は行わない)".Tell("00ffff");
        }
    }
}