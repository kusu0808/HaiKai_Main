using Cysharp.Threading.Tasks;
using Main;
using System.Threading;
using UnityEngine.SceneManagement;

namespace General
{
    public static class Scene
    {
        public enum ID
        {
            Title,
            Main,
            Result
        }

        /// <summary>
        /// キャンセル不可
        /// </summary>
        public static async UniTaskVoid LoadAsync(this ID id)
        {
            string name = id.ToName();
            if (string.IsNullOrEmpty(name)) return;

            PauseState.IsPaused = true;
            var opr = SceneManager.LoadSceneAsync(name);
            opr.allowSceneActivation = false;
            await UniTask.WaitUntil(() => opr.progress >= 0.9f);
            opr.allowSceneActivation = true;
            await UniTask.WaitUntil(() => opr.isDone);
            PauseState.IsPaused = false;
        }

        private static string ToName(this ID id) => id switch
        {
            ID.Title => "Title",
            ID.Main => "Main",
            ID.Result => "Result",
            _ => string.Empty
        };
    }
}
