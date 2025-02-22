using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void VillageHousePickUpSecretKey()
        {
            if (_objects.KokeshiSecretKey.IsEnabled is false) return;


            _uiElements.LogText.ShowAutomatically("隠された鍵を入手した");
            _uiElements.KokeshiSecretKey.Obtain();
            _audioSources.GetNew().Raise(_audioClips.SE.ObtainItem, SoundType.SE);
            _objects.KokeshiSecretKey.IsEnabled = false;

            _isPickUpSecretKeyEventEnabled = false;
        }
    }
}