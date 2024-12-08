using General;
using IvyType = Main.Eventer.Objects.BigIviesClass.Type;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void CutBigIvy(IvyType type)
        {
            if (_uiElements.DaughterKnife.IsHolding() is true)
            {
                _audioSources.GetNew().Raise(_audioClips.SE.CutBigIvy, SoundType.SE);
                _objects.BigIvies.DeactivateThis(type);
                _uiElements.LogText.ShowAutomatically("通れるようになった");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("大きな植物が道を遮っている");
            }
        }
    }
}