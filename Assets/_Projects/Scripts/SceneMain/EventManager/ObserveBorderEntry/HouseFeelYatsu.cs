using System.Threading;
using Cysharp.Threading.Tasks;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private async UniTaskVoid HouseFeelYatsu(CancellationToken ct)
        {
            await UniTask.WaitUntil(() => _borders.HouseFeelingYatsu1.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.SE.FeelingYatsuInHouse1, SoundType.SE);
            await UniTask.WaitUntil(() => _borders.HouseFeelingYatsu2.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.SE.FeelingYatsuInHouse2, SoundType.SE);
            await UniTask.WaitUntil(() => _borders.HouseFeelingYatsu3.IsIn(_player.Position) is true, cancellationToken: ct);
            _audioSources.GetNew().Raise(_audioClips.SE.FeelingYatsuInHouse3, SoundType.SE);
        }
    }
}