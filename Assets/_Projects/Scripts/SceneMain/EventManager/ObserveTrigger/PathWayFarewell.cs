using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid PathWayFarewell(CancellationToken ct)
        {
            //_daughter.SetPathWayItemsEnabled(false);
            "本来は、アイテムを消しておくこと！".Warn();
            await UniTask.WaitUntil(() => _borders.PathWayFarewell.IsIn(_player.Position) is true, cancellationToken: ct);
            _uiElements.NewlyShowLogText("キャーッ！", EventManagerConst.EventTextShowDuration, false);
            _daughter.IsActive = false;
            _daughter.SetPathWayItemsEnabled(true);
            _audioSources.GetNew().Raise(_audioClips.Voice.DaughterScream, SoundType.Voice);
            "何か演出も入れるか？".Warn();
        }
    }
}