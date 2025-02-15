using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehousePickUpCup()
        {
            if (_objects.ToiletCup.IsEnabled is false) return;

            _uiElements.LogText.ShowAutomatically("コップを入手した");
            _uiElements.Cup.Obtain();
            _audioSources.GetNew().Raise(_audioClips.SE.ObtainItem, SoundType.SE);
            _objects.ToiletCup.IsEnabled = false;
        }
    }
}