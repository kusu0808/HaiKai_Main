using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageFarWayCrashCup()
        {
            if (_hasCrashedCup is true) return;

            if (_uiElements.Cup.IsHolding() is true)
            {
                _audioSources.GetNew().Raise(_audioClips.SE.CupBreaking, SoundType.SE);
                _uiElements.LogText.ShowAutomatically("コップを割った");
                _uiElements.Cup.Release();
                _uiElements.GlassShard.Obtain();
                _hasCrashedCup = true;
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("何かをたたき割れそうだ");
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("何かをたたき割れそうだ");
            }
        }
    }
}