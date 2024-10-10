using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General
{
    /// <summary>
    /// シーンのID
    /// </summary>
    public enum SceneID
    {
        Title,
        Main,
        Result
    }

    /// <summary>
    /// シーン変更担当クラス
    /// </summary>
    public static class SceneChange
    {
        /// <summary>
        /// フェードアウト後非同期シーンロード
        /// </summary>
        public static async UniTask FadeOutAndChangeScene(SceneID sceneId, CancellationToken cancellationToken)
        {
            // await FadeOut(cancellationToken); // フェードアウトの実装はこちらに

            await LoadSceneAsync(sceneId, cancellationToken);
        }

        /// <summary>
        /// 非同期シーンロード
        /// </summary>
        public static async UniTask LoadSceneAsync(SceneID sceneId, CancellationToken cancellationToken)
        {
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneId.ToSceneName());
            sceneLoadOperation.allowSceneActivation = false;
            await UniTask.WaitUntil(() => sceneLoadOperation.isDone, cancellationToken: cancellationToken);
            sceneLoadOperation.allowSceneActivation = true;
        }

        private static string ToSceneName(this SceneID sceneId) => sceneId switch
        {
            SceneID.Title => "Title",
            SceneID.Main => "Main",
            SceneID.Result => "Result",
            _ => throw new(),
        };
    }
}