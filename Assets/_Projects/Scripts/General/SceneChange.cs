using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General 
{
    /// <summary>
    /// シーン変更担当クラス
    /// </summary>
    internal sealed class SceneChange 
    {
        /// <summary>
        /// フェードアウト後シーン変更
        /// </summary>
        /// <param name="sceneName">遷移先のシーン名</param>
        /// <returns>UniTask</returns>
        internal async UniTask FadeOutAndChangeScene(string sceneName)
        {
            //await FadeOut();フェードアウトの実装はこちらに

            await LoadSceneAsync(sceneName);
        }

        /// <summary>
        /// UniTaskを用いたシーンロード
        /// </summary>
        /// <param name="sceneName">遷移先のシーン名</param>
        /// <returns>UniTask</returns>
        private async UniTask LoadSceneAsync(string sceneName)
        {
            AsyncOperation sceneLoadOperation = SceneManager.LoadSceneAsync(sceneName);
            while (sceneLoadOperation.isDone == false) await UniTask.Yield();
        }
    }
}


