using General;

namespace Main.EventManager
{
    public sealed partial class EventManager
    {
        private void WarehouseOpenLockedDoor()
        {
            if (_uiElements.WarehouseKey.IsHolding() is true)
            {
                _uiElements.WarehouseKey.Release();

                _objects.WarehouseLockedDoor.Trigger();
                _uiElements.LogText.ShowAutomatically("鍵を開けた");
                _audioSources.GetNew().Raise(_audioClips.SE.KeyOpen, SoundType.SE);
                _audioSources.GetNew().Raise(_audioClips.SE.OpenWoodSlideDoor, SoundType.SE);
            }
            else if (_uiElements.IsHoldingAnyItem() is true)
            {
                _uiElements.LogText.ShowAutomatically("鍵を開けられるものはないだろうか？");
                _audioSources.GetNew().Raise(_audioClips.SE.OpenWoodUnopenableDoor, SoundType.SE);
            }
            else
            {
                _uiElements.LogText.ShowAutomatically("鍵がかかっている");
                _audioSources.GetNew().Raise(_audioClips.SE.OpenWoodUnopenableDoor, SoundType.SE);
            }
        }
    }
}