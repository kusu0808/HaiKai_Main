using System.Threading;
using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageFarWayPickUpDoubledKey()
        {
            if (_objects.WarehouseKeyDoubled.IsEnabled is false) return;

            _uiElements.LogText.ShowAutomatically("2つの鍵を入手した");
            _uiElements.WarehouseKeyDoubled.Obtain();
            _audioSources.GetNew().Raise(_audioClips.SE.ObtainItem, SoundType.SE);
            _objects.WarehouseKeyDoubled.IsEnabled = false;
        }
    }
}