using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageFarWayCrashCup()
        {
            if (_uiElements.Cup.IsHolding() is true)
            {
                _audioSources.GetNew().Raise(_audioClips.SE.CupBreaking, SoundType.SE);
                _uiElements.LogText.ShowAutomatically("コップを割った");
                _uiElements.Cup.Release();
                _uiElements.GlassShard.Obtain();
            }
        }
    }
}