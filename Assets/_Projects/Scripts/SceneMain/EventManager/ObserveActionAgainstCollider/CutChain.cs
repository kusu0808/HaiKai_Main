using System.Threading;
using Cysharp.Threading.Tasks;
using Type = Main.Eventer.Objects.DaughterChainClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid CutChain(Type type, CancellationToken ct)
        {
            var chain = _objects.DaughterChain;

            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                chain.Cut(type);

                if (chain.IsAllCut() is false) return;

                await _uiElements.BlackImage.FadeOut(EventManagerConst.FadeOutDuration, ct);
                _objects.ShrineChainedDaughter.IsEnabled = false;
                _objects.ShrineUpWayCannotGoAtLastEscape.IsEnabled = true;
                _objects.PathWayCannotGoAtLastEscape.IsEnabled = true;
                _daughter.SpawnHere(_points.ShrineDaughterSpawnPoint);
                _daughter.ChangeAnimationModeFromEnterToEscape();
                await _uiElements.BlackImage.FadeIn(EventManagerConst.FadeInDuration, ct);
                _hasSavedDaughter = true;
            }
        }
    }
}