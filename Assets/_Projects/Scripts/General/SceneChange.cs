using Cysharp.Threading.Tasks;
using Main;
using UnityEngine.SceneManagement;

namespace General
{
    public static class Scene
    {
        public enum ID
        {
            Title,
            Main,
            Death,
            Result
        }

        private static bool _isLoading = false;

        /// <summary>
        /// キャンセル不可
        /// </summary>
        public static async UniTaskVoid LoadAsync(this ID id)
        {
            if (_isLoading) return;
            string name = id.ToName();
            if (string.IsNullOrEmpty(name)) return;

            _isLoading = true;
            PauseState.IsPaused = true;
            var opr = SceneManager.LoadSceneAsync(name);
            opr.allowSceneActivation = false;
            await UniTask.WaitUntil(() => opr.progress >= 0.9f);
            opr.allowSceneActivation = true;
            await UniTask.WaitUntil(() => opr.isDone);
            PauseState.IsPaused = false;
            _isLoading = false;
        }

        private static string ToName(this ID id) => id switch
        {
            ID.Title => "Title",
            ID.Main => "Main",
            ID.Death => "Death",
            ID.Result => "Result",
            _ => string.Empty
        };
    }
}
