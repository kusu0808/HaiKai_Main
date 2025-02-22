using System.Threading;
using General;
using IvyType = Main.Eventer.Objects.BigIviesClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void GeneralCutBigIvy(IvyType type, CancellationToken ctIfNeeded)
        {
            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                _audioSources.GetNew().Raise(_audioClips.SE.CutBigIvy, SoundType.SE);
                _objects.BigIvies.DeactivateThis(type);
                _uiElements.LogText.ShowAutomatically("通れるようになった");

                if (type == IvyType.PathWay) _postProcessManager.DoGameStartTransition(ctIfNeeded).Forget();
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("鋭利なものが必要だ");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("大きな植物が道を遮っている");
            }
        }
    }
}