using Cysharp.Threading.Tasks;
using System.Threading;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid FootOnDishOnce(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.FootOnDish.IsIn(_player.Position) is true, cancellationToken: ct);
            _uiElements.NewlyShowLogText("キャッ！", EventManagerConst.NormalTextShowDuration, false);
            _audioSources.GetNew().Raise(_audioClips.SE.DishBreak, SoundType.SE);
        }
    }
}
