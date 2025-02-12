using System.Threading;
using Cysharp.Threading.Tasks;
using General;
using Type = Main.Eventer.Objects.DaughterChainClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid ShrineCutDaughterChain(Type type, CancellationToken ct)
        {
            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                var chain = _objects.DaughterChain;

                chain.Cut(type);
                _audioSources.GetNew().Raise(_audioClips.SE.CutDaugherChain, SoundType.SE);

                if (chain.IsAllCut() is false) return;

                await _uiElements.BlackImage.FadeOut(EventManagerConst.FadeOutDuration, ct);
                _objects.ShrineChainedDaughter.IsEnabled = false;
                _objects.ShrineUpWayCannotGoAtLastEscape.IsEnabled = true;
                _objects.PathWayCannotGoAtLastEscape.IsEnabled = true;
                _daughter.SpawnHere(_points.ShrineDaughterSpawnPoint);
                _daughter.BecomeEmergencyMode();
                await _uiElements.BlackImage.FadeIn(EventManagerConst.FadeInDuration, ct);
                _hasSavedDaughter = true;
                _objects.ShrineCannotGetOutUntilDaughterSaved.IsEnabled = false;
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("鋭利なものが必要だ");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("鎖が娘の体をキツく締め上げている");
            }
        }
    }
}