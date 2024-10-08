using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンのID
/// </summary>
internal enum SceneID
{
    Start,
    Game
}

namespace General 
{
    /// <summary>
    /// シーン変更担当クラス
    /// </summary>
    internal static class SceneChange 
    {
        /// <summary>
        /// フェードアウト後シーン変更
        /// </summary>
        /// <param name="sceneId">遷移先のシーンID</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>UniTask</returns>
        internal static async UniTask FadeOutAndChangeScene(SceneID sceneId, CancellationToken cancellationToken)
        {
            // await FadeOut(cancellationToken); // フェードアウトの実装はこちらに

            await LoadSceneAsync(sceneId, cancellationToken);
        }

        /// <summary>
        /// UniTaskを用いたシーンロード
        /// </summary>
        /// <param name="sceneId">遷移先のシーン名</param>
        /// <param name="cancellationToken">キャンセルトークン</param>
        /// <returns>UniTask</returns>
        private static async UniTask LoadSceneAsync(SceneID sceneId, CancellationToken cancellationToken)
        {
            string sceneName = sceneId.ToString();
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
            sceneLoadOperation.allowSceneActivation = false;
            await UniTask.WaitUntil(() => sceneLoadOperation.isDone, cancellationToken: cancellationToken);
            sceneLoadOperation.allowSceneActivation = true;
        }
    }
}


