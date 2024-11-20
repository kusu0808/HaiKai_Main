using General;
using Type = Main.Eventer.Objects.BigIviesClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void CutBigIvy(Type tpye)
        {
            if (_uiElements.IsHoldingDaughterKnife())
            {
                _audioSources.GetNew().Raise(_audioClips.SE.CutBigIvy, SoundType.SE);
                _objects.BigIvies.DeactivateThis(tpye);
                _uiElements.NewlyShowLogText("通れるようになった");
            }
            else
            {
                _uiElements.NewlyShowLogText("大きな植物が道を遮っている");
            }
        }
    }
}